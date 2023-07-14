using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Persistence.Abstractions
{
    public interface IDataRepository : IGenericRepository<Data>
    {
        /// <summary>
        /// Получить срез списка с данными 
        /// </summary>
        /// <param name="start">Начало среза</param>
        /// <param name="length">Длина среза</param>
        /// <param name="onlyValid">Нужно ли взять только проверенные данные</param>
        /// <param name="email">Часть электронной почты</param>
        /// <param name="dateStart">Начало отчета даты</param>
        /// <param name="dateEnd">Конец отчета даты</param>
        /// <returns>Список с данными</returns>
        public Task<IQueryable<Data>> Get(
            int start = 0,
            int length = int.MaxValue,
            bool onlyValid = false,
            string? email = null,
            DateTime? dateStart = null,
            DateTime? dateEnd = null);
    }
}
