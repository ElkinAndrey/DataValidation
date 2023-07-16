using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Service.Abstractions;

namespace DataValidationAPI.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
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
