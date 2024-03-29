﻿using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Persistence.Abstractions
{
    /// <summary>
    /// Интерфейс универсального репозитория
    /// </summary>
    /// <typeparam name="TEntity">Сущность репозитория</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Получить все
        /// </summary>
        /// <returns>Список сущностей</returns>
        public Task<IQueryable<TEntity>> Get();

        /// <summary>
        /// Получить по Id
        /// </summary>
        /// <param name="id">Id сущности</param>
        /// <returns>Сущность</returns>
        public Task<TEntity> GetById(Guid id);

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        public Task Insert(TEntity entity);

        /// <summary>
        /// Удалить по Id
        /// </summary>
        /// <param name="id">Id удаляемой сущности<</param>
        public Task Delete(Guid id);

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="entity">Изменяемая сущность</param>
        public Task Update(TEntity entity);

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public Task Save();
    }
}
