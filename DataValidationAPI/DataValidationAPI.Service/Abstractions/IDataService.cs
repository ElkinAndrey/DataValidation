using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Service.Services
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
        /// <param name="email">Часть электронной почты</param>
        /// <param name="dateStart">Начало отчета даты</param>
        /// <param name="dateEnd">Конец отчета даты</param>
        /// <returns>Список со всеми данными</returns>
        public Task<IEnumerable<Data>> GetDatasAsync(
            int start = 0,
            int length = int.MaxValue,
            string? email = null,
            DateTime? dateStart = null,
            DateTime? dateEnd = null);

        /// <summary>
        /// Получить проверенных данных
        /// </summary>
        /// <param name="start">Начало отчета</param>
        /// <param name="length">Длина среза</param>
        /// <param name="email">Часть электронной почты</param>
        /// <param name="dateStart">Начало отчета даты</param>
        /// <param name="dateEnd">Конец отчета даты</param>
        /// <returns>Список с проверенными данныхм</returns>
        public Task<IEnumerable<Data>> GetValidDatasAsync(
            int start,
            int length,
            int email,
            DateTime dateStart,
            DateTime dateEnd);

        /// <summary>
        /// Добавить не проверенные данные
        /// </summary>
        /// <param name="personId">Id человека, который оставил данные</param>
        /// <param name="information">Информация</param>
        public Task AddNoValidDatesAsync(Guid personId, string information);

        /// <summary>
        /// Выдать оценку данным
        /// </summary>
        /// <param name="dataId">Id данных</param>
        /// <param name="valid">Как проверено</param>
        public Task RateDataAsync(
            Guid dataId,
            bool? valid = null);
    }
}
