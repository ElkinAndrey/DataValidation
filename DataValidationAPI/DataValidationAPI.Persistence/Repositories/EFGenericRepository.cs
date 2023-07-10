﻿using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;

namespace DataValidationAPI.Persistence.Repositories
{
    /// <summary>
    /// Универсальный репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Сущность репозитория</typeparam>
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<TEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}