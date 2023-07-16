using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Dto;

namespace DataValidationAPI.Persistence.Abstractions
{
    public interface IDataRepository : IGenericRepository<Data>
    {
        /// <summary>
        /// Получить данные
        /// </summary>
        /// <param name="param">Параметры для получения данных</param>
        /// <returns>Список данных</returns>
        public Task<IEnumerable<Data>> Get(GetDataFromRepositoryParams param);
    }
}
