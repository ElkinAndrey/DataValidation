using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Dto;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Dto;
using DataValidationAPI.Service.Exceptions;

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
        /// <param name="dataCheckRepository">Репозиторий с проверками данных</param>
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

        public async Task ChangeDataAsync(
            Guid id,
            string? information = null,
            Guid? personProvidedId = null,
            Guid? userId = null)
        {
            var data = await _dataRepository.GetById(id);

            if (userId is not null && data.PersonProvidedId != userId)
                throw new NoPermissionToAccessDataException();

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

        public async Task DeleteDataAsync(Guid dataId, Guid? userId = null)
        {
            if (userId is not null)
            {
                var data = await _dataRepository.GetById(dataId);
                if (data.PersonProvidedId != userId)
                    throw new NoPermissionToAccessDataException();
            }

            await _dataRepository.Delete(dataId);
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
                    if (data.PersonProvided!.Id == user.Id ||
                        (data.DataCheck is not null && data.DataCheck.Valid == true))
                        return data;
                    break;
                case Roles.Manager:
                case Roles.Admin:
                    return data;
            }

            throw new NoPermissionToAccessDataException();
        }

        public async Task<int> GetDataCountAsync(GetDataCountParams param)
        {
            var data = await GetDatasIQueryableAsync(new GetDataParams()
            {
                Start = 0,
                Length = int.MaxValue,
                Email = param.Email,
                DateStart = param.DateStart,
                DateEnd = param.DateEnd,
                IsUnverifiedData = param.IsUnverifiedData,
                IsValidatedData = param.IsValidatedData,
                IsNoValidatedData = param.IsNoValidatedData,
                IsCheckData = param.IsCheckData,
                UserParam = param.UserParam is null ? null : new UserParam()
                {
                    UserId = param.UserParam.UserId,
                    RecipientDataId = param.UserParam.UserId,
                    RoleRecipientData = param.UserParam.RoleRecipientData,
                    TakeOnlyThisUser = param.UserParam.TakeOnlyThisUser,
                }
            });

            return data.Count();
        }

        public async Task<IEnumerable<Data>> GetDatasAsync(GetDataParams param)
        {
           var data = await GetDatasIQueryableAsync(param);

            return data;
        }

        private async Task<IQueryable<Data>> GetDatasIQueryableAsync(GetDataParams param)
        {
            var repParam = new GetDataFromRepositoryParams()
            {
                Start = param.Start,
                Length = param.Length,
                Email = param.Email,
                DateStart = param.DateStart,
                DateEnd = param.DateEnd,
                IsUnverifiedData = false,
                IsValidatedData = param.IsValidatedData,
                IsNoValidatedData = false,
                IsCheckData = false,
                UserParam = null,
            };

            if (param.UserParam?.RoleRecipientData is not null)
            {
                repParam.UserParam = new UserParamForRepository
                {
                    UserId = param.UserParam.UserId,
                    TakeOnlyThisUser = param.UserParam.TakeOnlyThisUser,
                    IsUnverifiedData = param.IsUnverifiedData,
                    IsValidatedData = param.IsValidatedData,
                    IsNoValidatedData = param.IsNoValidatedData,
                    IsCheckData = param.IsCheckData,
                };

                switch (param.UserParam.RoleRecipientData)
                {
                    default:
                        if (param.UserParam.RecipientDataId != param.UserParam.UserId)
                            repParam.UserParam = new UserParamForRepository
                            {
                                UserId = param.UserParam.UserId,
                                TakeOnlyThisUser = param.UserParam.TakeOnlyThisUser,
                                IsUnverifiedData = false,
                                IsValidatedData = param.IsValidatedData,
                                IsNoValidatedData = false,
                                IsCheckData = false,
                            };
                        break;
                    case Roles.Manager:
                    case Roles.Admin:
                        repParam.IsUnverifiedData = param.IsUnverifiedData;
                        repParam.IsValidatedData = param.IsValidatedData;
                        repParam.IsNoValidatedData = param.IsNoValidatedData;
                        repParam.IsCheckData = param.IsCheckData;
                        break;
                }
            }

            var data = await _dataRepository.Get(repParam);

            return data;
        }

        public async Task RateDataAsync(
            Guid dataId,
            Guid userId,
            bool? valid = null)
        {
            var dataCheck = await _dataCheckRepository.GetById(dataId);
            var user = await _userRepository.GetById(userId);

            if (dataCheck is not null)
                await _dataCheckRepository.Delete(dataId);

            var data = await _dataRepository.GetById(dataId);
            await _dataCheckRepository.Insert(new DataCheck()
            {
                Data = data,
                User = user,
                Valid = valid
            });
            await _dataCheckRepository.Save();
        }
    }
}
