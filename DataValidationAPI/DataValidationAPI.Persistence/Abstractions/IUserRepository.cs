using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Persistence.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория пользователей
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Получить пользователя по электронной почте
        /// </summary>
        /// <param name="email">Электронная почта</param>
        /// <returns>Пользователь (null если не найден)</returns>
        public Task<User?> GetByEmail(string email);

        /// <summary>
        /// Получить пользователя по токену обновления
        /// </summary>
        /// <param name="token">Токен обновления</param>
        /// <returns>Пользователь</returns>
        public Task<User?> GetByToken(string token);
    }
}
