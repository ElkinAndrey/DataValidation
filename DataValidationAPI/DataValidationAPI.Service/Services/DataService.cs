﻿using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Repositories;
using DataValidationAPI.Service.Abstractions;
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

        public async Task<IEnumerable<Data>> GetDatasAsync(
            int start = 0,
            int length = int.MaxValue,
            bool onlyValid = false,
            string? email = null,
            DateTime? dateStart = null,
            DateTime? dateEnd = null)
        {
            var data = await _dataRepository.Get(
                start,
                length,
                onlyValid,
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