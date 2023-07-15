using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Repositories;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Dto;
using DataValidationAPI.Service.Exceptions;
using DataValidationAPI.Service.Features;
using System;
using System.Security.Claims;

namespace DataValidationAPI.Service.Services
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Репозиторий с пользователями
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// Репозиторий с ролями
        /// </summary>
        private IRoleRepository _roleRepository;

        /// <summary>
        /// Сервис авторизации
        /// </summary>
        /// <param name="userRepository">Репозиторий с пользователями</param>
        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task DeleteTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<PairOfTokens> LoginAsync(string email, string password, string secretKey)
        {
            // Пользователь ищется в базе данных
            var user = await _userRepository.GetByEmail(email);

            // Если пользователь с таким Email не найден
            if (user is null)
                throw new EmailNotFoundException(email);

            // Проверяется, правильно ли введен пароль
            if (!PasswordHash.Verify(password, user?.PasswordHash!, user?.PasswordSalt!))
                throw new WrongPasswordException();

            var tokens = await GenerateTokens(user!, secretKey);

            user!.RefreshToken = tokens.RefreshToken;
            user!.TokenExpirationDate = tokens.GenerationDate + JwtLifetime.RefreshTimeSpan;

            await _userRepository.Update(user);
            await _userRepository.Save();

            return tokens;
        }

        public async Task<PairOfTokens> RefreshTokenAsync(string token, string secretKey)
        {
            throw new NotImplementedException();
        }

        public async Task<PairOfTokens> RegisterAsync(
            string email,
            string password,
            string secretKey)
        {
            // Есть ли такой Email
            if (await _userRepository.GetByEmail(email) is not null)
                throw new EmailAlreadyExistsException(email);

            string passwordHash; // Хэш пароля
            byte[] passwordSalt; // Соль пароля

            // Хэшируется пароль
            PasswordHash.Create(password, out passwordHash, out passwordSalt);

            // Создается новый пользователь
            Role role = await _roleRepository.GetByName(Roles.User)
                ?? new Role { Id = Guid.NewGuid(), Name = Roles.User };
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email, // Имя
                Role = role,
                PasswordHash = passwordHash, // Хэш пароля
                PasswordSalt = passwordSalt, // Соль пароля
            };

            var tokens = await GenerateTokens(user, secretKey);

            user.RefreshToken = tokens.RefreshToken;
            user.TokenExpirationDate = tokens.GenerationDate + JwtLifetime.RefreshTimeSpan;

            await _userRepository.Insert(user);
            await _userRepository.Save();

            return tokens;
        }

        private async Task<PairOfTokens> GenerateTokens(User user, string secretKey)
        {
            // Данные, которые будут записаны в токен
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Имя пользователя
                new Claim(ClaimTypes.Email, user.Email), // Имя пользователя
                new Claim(ClaimTypes.Role, user.Role.Name) // Роль пользователя
            };

            // Генерируется токен доступа (часто обновляется)
            var accessToken = JwtTokens.CreateAccessToken(claims, secretKey);

            // Генерируется токен обновления (редко обновляется)
            var refreshToken = JwtTokens.CreateRefreshToken();

            // Время создания токенов
            var time = DateTime.Now;

            return new PairOfTokens()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                GenerationDate = time,
            };
        }
    }
}
