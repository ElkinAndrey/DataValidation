using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Dto;
using DataValidationAPI.Service.Dto;

namespace DataValidationAPI.Service.Abstractions
{
    /// <summary>
    /// Сервис для работы с данными
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Получить данные
        /// </summary>
        /// <param name="param">Параметры для получения данных</param>
        /// <returns>Список данных</returns>
        public Task<IEnumerable<Data>> GetDatasAsync(GetDataParams param);

        /// <summary>
        /// Добавить не проверенные данные
        /// </summary>
        /// <param name="userId">Id человека, который оставил данные</param>
        /// <param name="information">Информация</param>
        /// <param name="dataId">Id новых данных</param>
        public Task AddNoValidDatesAsync(
            Guid userId,
            string information,
            Guid? dataId = null);

        /// <summary>
        /// Выдать оценку данным
        /// </summary>
        /// <param name="dataId">Id данных</param>
        /// <param name="userId">Id пользователя, выдающего оценку</param>
        /// <param name="valid">Как проверено</param>
        public Task RateDataAsync(
            Guid dataId,
            Guid userId,
            bool? valid = null);

        /// <summary>
        /// Получить данные по Id
        /// </summary>
        /// <param name="dataId">Id данных</param>
        /// <param name="user">Пользователь, который вошел в аккаунт</param>
        public Task<Data> GetDataByIdAsync(Guid dataId, User? user);

        /// <summary>
        /// Удалить данные по Id
        /// </summary>
        /// <remarks>
        /// Если указан id пользователя, то данные удаляться только в том случае, если id совпадает с id пользователя, 
        /// добавившего данные
        /// </remarks>
        /// <param name="dataId">Id данных</param>
        /// <param name="userId">Id пользователя</param>
        public Task DeleteDataAsync(Guid dataId, Guid? userId = null);

        /// <summary>
        /// Изменить данные
        /// </summary>
        /// <remarks>
        /// Если указан id пользователя, то данные изменяются только в том случае, если id совпадает с id пользователя, 
        /// добавившего данные
        /// </remarks>
        /// <param name="id">Id данных</param>
        /// <param name="information">Новая информация</param>
        /// <param name="personProvidedId">Новый человек</param>
        /// <param name="userId">Id пользователя</param>
        public Task ChangeDataAsync(
            Guid id,
            string? information = null,
            Guid? personProvidedId = null,
            Guid? userId = null);
    }
}
