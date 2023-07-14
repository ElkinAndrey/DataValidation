using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Persistence.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория ролей
    /// </summary>
    public interface IRoleRepository : IGenericRepository<Role>
    {
        /// <summary>
        /// Получить роль по названию
        /// </summary>
        /// <param name="name">Название роли</param>
        /// <returns>Роль (null - если не найдено)</returns>
        public Task<Role?> GetByName(string name);
    }
}
