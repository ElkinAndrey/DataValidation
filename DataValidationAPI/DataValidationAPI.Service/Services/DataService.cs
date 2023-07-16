using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Repositories;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Exceptions;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private IDataRepository _dataRepository;

        /// <summary>
        /// Репозиторий проверки данных
        /// </summary>
        private IDataCheckRepository _dataCheckRepository;

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
            IDataRepository dataRepository,
            IDataCheckRepository dataCheckRepository,
            IGenericRepository<User> userRepository)
        {
            _dataRepository = dataRepository;
            _dataCheckRepository = dataCheckRepository;
            _userRepository = userRepository;
        }

        public async Task AddNoValidDatesAsync(
            Guid userId,
            string information,
            Guid? dataId = null)
        {
            var user = await _userRepository.GetById(userId);

            var data = new Data
            {
                Id = dataId is null ? Guid.NewGuid() : (Guid)dataId,
                Date = DateTime.Now,
                Information = information,
                PersonProvided = user,
            };

            await _dataRepository.Insert(data);
            await _dataRepository.Save();
        }

        public async Task ChangeDataAsync(Guid id, string? information = null, Guid? personProvidedId = null)
        {
            var data = await _dataRepository.GetById(id);
            if (information is not null)
                data.Information = information;
            if (personProvidedId is not null)
            {
                var user = await _userRepository.GetById((Guid)personProvidedId);
                data.PersonProvided = user;
            }
            await _dataRepository.Update(data);
            await _dataRepository.Save();
        }

        public async Task DeleteDataAsync(Guid id)
        {
            await _dataRepository.Delete(id);
            await _dataRepository.Save();
        }

        public async Task<Data> GetDataByIdAsync(Guid dataId, User? user)
        {
            var data = await _dataRepository.GetById(dataId);

            switch (user?.Role?.Name!)
            {
                default:
                    if (data.DataCheck is not null && data.DataCheck.Valid == true)
                        return data;
                    break;
                case Roles.User:
                    if (data.PersonProvided.Id == user.Id ||
                        (data.DataCheck is not null && data.DataCheck.Valid == true))
                        return data;
                    break;
                case Roles.Manager:
                case Roles.Admin:
                    return data;
            }

            throw new NoPermissionToAccessDataException();
        }

        public async Task<IEnumerable<Data>> GetDatasAsync(
            int start = 0,
            int length = int.MaxValue,
            User? user = null,
            string? email = null,
            DateTime? dateStart = null,
            DateTime? dateEnd = null)
        {
            var onlyValid = true;
            var userId = (Guid?)null;

            switch (user?.Role?.Name!)
            {
                default:
                    onlyValid = true;
                    userId = null;
                    break;
                case Roles.User:
                    userId = user.Id;
                    onlyValid = true;
                    break;
                case Roles.Manager:
                case Roles.Admin:
                    onlyValid = false;
                    userId = null;
                    break;
            }

            var data = await _dataRepository.Get(
                start,
                length,
                onlyValid,
                userId,
                email,
                dateStart,
                dateEnd);

            return data;
        }

        public async Task RateDataAsync(
            Guid dataId,
            Guid userId,
            bool? valid = null)
        {
            var dataCheck = await _dataCheckRepository.GetById(dataId);
            var user = await _userRepository.GetById(userId);

            if (dataCheck is null)
            {
                var data = await _dataRepository.GetById(dataId);
                await _dataCheckRepository.Insert(new DataCheck()
                {
                    Data = data,
                    User = user,
                    Valid = valid
                });
                await _dataCheckRepository.Save();
                return;
            }

            dataCheck.Valid = valid;
            dataCheck.User = user;
            await _dataCheckRepository.Update(dataCheck);
            await _dataCheckRepository.Save();
        }
    }
}
