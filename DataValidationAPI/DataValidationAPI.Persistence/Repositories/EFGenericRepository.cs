using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    /// <summary>
    /// Универсальный репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Сущность репозитория</typeparam>
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Нужная таблица
        /// </summary>
        private DbSet<TEntity> _set;

        /// <summary>
        /// Универсальный репозиторий
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EFGenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public virtual async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _set.Remove(entity);
        }

        public virtual async Task<IQueryable<TEntity>> Get()
        {
            return await Task.Run(() =>
            {
                var urls = _set.AsQueryable();
                return urls;
            });
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var entity = await _set.FirstOrDefaultAsync(e => e.Id == id);

            if (entity is null)
                throw new EntityNotFoundException(id);

            return entity;
        }

        public virtual async Task Insert(TEntity entity)
        {
            await _set.AddAsync(entity);
        }

        public virtual async Task Update(TEntity entity)
        {
            await Task.Run(() =>
            {
                _set.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            });
        }

        public virtual async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
