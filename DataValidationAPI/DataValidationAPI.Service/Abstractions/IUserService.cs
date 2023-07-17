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

        /// <summary>
        /// Заблокировать пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        public Task DeleteUserAsync(Guid userId);

        /// <summary>
        /// Поменять состояние блокировки пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="isActive">Новое состояние</param>
        public Task ChangeBlockUserAsync(Guid userId, bool isActive);

        /// <summary>
        /// Изменить электронную почту
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="newEmail">Новая почта</param>
        public Task ChangeEmailAsync(Guid userId, string newEmail);

        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="newPassword">Новый пароль</param>
        public Task ChangePasswordAsync(Guid userId, string newPassword);
    }
}
