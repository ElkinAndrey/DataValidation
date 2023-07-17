using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Repositories;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Exceptions;
using DataValidationAPI.Service.Features;

namespace DataValidationAPI.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ChangeBlockUserAsync(Guid userId, bool isActive)
        {
            var user = await _repository.GetById(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            user.IsActive = isActive;

            await _repository.Update(user);
            await _repository.Save();
        }

        public async Task ChangeEmailAsync(Guid userId, string newEmail)
        {
            // Есть ли такой Email
            if (await _repository.GetByEmail(newEmail) is not null)
                throw new EmailAlreadyExistsException(newEmail);

            var user = await _repository.GetById(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            user.Email = newEmail;
            user.RefreshToken = null;
            user.TokenExpirationDate = null;

            await _repository.Update(user);
            await _repository.Save();
        }

        public async Task ChangePasswordAsync(Guid userId, string newPassword)
        {
            var user = await _repository.GetById(userId);

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

            await _repository.Update(user);
            await _repository.Save();
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _repository.GetById(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            await _repository.Delete(user.Id);
            await _repository.Save();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await _repository.GetById(userId);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(
            int start = 0,
            int length = int.MaxValue,
            string? email = null,
            Guid? roleId = null,
            DateTime? startRegistrationDate = null,
            DateTime? endRegistrationDate = null)
        {
            var users = await _repository.Get(
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
