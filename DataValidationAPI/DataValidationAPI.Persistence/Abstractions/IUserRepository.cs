﻿using DataValidationAPI.Domain.Entities;

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
        public Task<IQueryable<User>> Get(
            int start = 0,
            int length = int.MaxValue,
            string? email = null,
            Guid? roleId = null,
            DateTime? startRegistrationDate = null,
            DateTime? endRegistrationDate = null);
    }
}
