using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Service.Abstractions
{
    /// <summary>
    /// Сервис для работы с данными
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Получить срез всех данных
        /// </summary>
        /// <param name="start">Начало отчета</param>
        /// <param name="length">Длина среза</param>
        /// <param name="user">Человек, у которого можно получить не проверенные данные</param>
        /// <param name="email">Часть электронной почты</param>
        /// <param name="dateStart">Начало отчета даты</param>
        /// <param name="dateEnd">Конец отчета даты</param>
        /// <returns>Список со всеми данными</returns>
        public Task<IEnumerable<Data>> GetDatasAsync(
            int start = 0,
            int length = int.MaxValue,
            User? user = null,
            string? email = null,
            DateTime? dateStart = null,
            DateTime? dateEnd = null);

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
        /// <param name="id">Id данных</param>
        public Task DeleteDataAsync(Guid id);

        /// <summary>
        /// Изменить данные
        /// </summary>
        /// <param name="id">Id данных</param>
        /// <param name="Information">Новая информация</param>
        /// <param name="PersonProvidedId">Новый человек</param>
        public Task ChangeDataAsync(
            Guid id,
            string? information = null,
            Guid? personProvidedId = null);
    }
}
