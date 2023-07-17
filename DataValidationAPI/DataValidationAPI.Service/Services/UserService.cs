using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Exceptions;

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
