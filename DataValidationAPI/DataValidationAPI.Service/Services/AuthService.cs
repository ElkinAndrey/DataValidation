using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Dto;

namespace DataValidationAPI.Service.Services
{
    public class AuthService : IAuthService
    {
        public Task DeleteTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<PairOfTokens> LoginAsync(string email, string password, string secretKey)
        {
            throw new NotImplementedException();
        }

        public Task<PairOfTokens> RefreshTokenAsync(string token, string secretKey)
        {
            throw new NotImplementedException();
        }

        public Task RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
