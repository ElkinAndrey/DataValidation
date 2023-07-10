using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Service.Abstractions;

namespace DataValidationAPI.Service.Services
{
    /// <summary>
    /// Сервис для работы с данными
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        /// Репозиторий с данными
        /// </summary>
        private IGenericRepository<Data> _dataRepository;

        /// <summary>
        /// Репозиторий с пользователями
        /// </summary>
        private IGenericRepository<User> _userRepository;

        /// <summary>
        /// Репозиторий с пользователями
        /// </summary>
        /// <param name="dataRepository">Репозиторий с данными</param>
        /// <param name="userRepository">Репозиторий с пользователями</param>
        public DataService(
            IGenericRepository<Data> dataRepository,
            IGenericRepository<User> userRepository)
        {
            _dataRepository = dataRepository;
            _userRepository = userRepository;
        }

        public Task AddNoValidDatesAsync(
            Guid personId,
            string information)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Data>> GetDatasAsync(
            int start = 0,
            int length = int.MaxValue,
            string? email = null,
            DateTime? dateStart = null,
            DateTime? dateEnd = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Data>> GetValidDatasAsync(
            int start,
            int length,
            int email,
            DateTime dateStart,
            DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public Task RateDataAsync(
            Guid dataId,
            bool? valid = null)
        {
            throw new NotImplementedException();
        }
    }
}
