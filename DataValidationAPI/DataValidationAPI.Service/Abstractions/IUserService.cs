using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Service.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса для работы с людьми
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <param name="start">Начало отчета</param>
        /// <param name="length">Длина среза</param>
        /// <param name="email">Электронная почта</param>
        /// <param name="roleId">Id роли</param>
        /// <param name="startRegistrationDate">Начало отчета даты регистрации</param>
        /// <param name="endRegistrationDate">Конец отчета даты регистрации</param>
        /// <returns>Список пользователей</returns>
        public Task<IEnumerable<User>> GetUsersAsync(
            int start = 0,
            int length = int.MaxValue,
            string? email = null,
            Guid? roleId = null,
            DateTime? startRegistrationDate = null,
            DateTime? endRegistrationDate = null);

        /// <summary>
        /// Получить пользователя по Id
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Пользователь</returns>
        public Task<User> GetUserByIdAsync(Guid userId);
    }
}
