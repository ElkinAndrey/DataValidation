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

        private ApplicationDbContext _context;
        private DbSet<TEntity> _set;

        public EFGenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _set.Remove(entity);
        }

        public async Task<IQueryable<TEntity>> Get()
        {
            var urls = await _set.ToListAsync();
            return urls.AsQueryable();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var entity = await _set.FirstOrDefaultAsync(e => e.Id == id);

            if (entity is null)
                throw new EntityNotFoundException(id);

            return entity;

        }

        public async Task Insert(TEntity entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task Update(TEntity entity)
        {
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
