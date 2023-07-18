using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Exceptions;
using DataValidationAPI.Service.Features;

namespace DataValidationAPI.Service.Services
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Репозиторий для работы с людьми
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// Репозиторий для работы с ролями
        /// </summary>
        private IRoleRepository _roleRepository;

        /// <summary>
        /// Сервис для работы с пользователями
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с людьми</param>
        /// <param name="roleRepository">Репозиторий для работы с ролями</param>
        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task ChangeBlockUserAsync(Guid userId, bool isActive)
        {
            var user = await _userRepository.GetById(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            user.IsActive = isActive;

            await _userRepository.Update(user);
            await _userRepository.Save();
        }

        public async Task ChangeEmailAsync(Guid userId, string newEmail)
        {
            // Есть ли такой Email
            if (await _userRepository.GetByEmail(newEmail) is not null)
                throw new EmailAlreadyExistsException(newEmail);

            var user = await _userRepository.GetById(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            user.Email = newEmail;
            user.RefreshToken = null;
            user.TokenExpirationDate = null;

            await _userRepository.Update(user);
            await _userRepository.Save();
        }

        public async Task ChangeManagerRoleAsync(Guid userId, bool isManager)
        {
            var user = await _userRepository.GetById(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            if (user.Role?.Name! == Roles.Admin)
                throw new InsufficientRightsException();

            Role role;
            if (isManager)
                role = await _roleRepository.GetByName(Roles.Manager)
                    ?? new Role { Id = Guid.NewGuid(), Name = Roles.Manager };
            else
                role = await _roleRepository.GetByName(Roles.User)
                    ?? new Role { Id = Guid.NewGuid(), Name = Roles.User };

            user.Role = role;
            await _userRepository.Update(user);
            await _userRepository.Save();
        }

        public async Task ChangePasswordAsync(Guid userId, string newPassword)
        {
            var user = await _userRepository.GetById(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            string passwordHash; // Хэш пароля
            byte[] passwordSalt; // Соль пароля

            // Хэшируется пароль
            PasswordHash.Create(newPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash; // Хэш пароля
            user.PasswordSalt = passwordSalt; // Соль пароля
            user.RefreshToken = null;
            user.TokenExpirationDate = null;

            await _userRepository.Update(user);
            await _userRepository.Save();
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            await _userRepository.Delete(user.Id);
            await _userRepository.Save();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            return user;
        }

        public async Task<int> GetUserCountAsync(
            string? email,
            Guid? roleId,
            DateTime? startRegistrationDate,
            DateTime? endRegistrationDate)
        {
            var users = await _userRepository.Get(
                start: 0,
                length: int.MaxValue,
                email: email,
                roleId: roleId,
                startRegistrationDate: startRegistrationDate,
                endRegistrationDate: endRegistrationDate);

            return users.Count();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(
            int start = 0,
            int length = int.MaxValue,
            string? email = null,
            Guid? roleId = null,
            DateTime? startRegistrationDate = null,
            DateTime? endRegistrationDate = null)
        {
            var users = await _userRepository.Get(
                start: start,
                length: length,
                email: email,
                roleId: roleId,
                startRegistrationDate: startRegistrationDate,
                endRegistrationDate: endRegistrationDate);

            return users;
        }
    }
}
