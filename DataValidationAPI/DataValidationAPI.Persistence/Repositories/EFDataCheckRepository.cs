using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    /// <summary>
    /// Репозиторий проверки данных
    /// </summary>
    public class EFDataCheckRepository : IDataCheckRepository
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Нужная таблица
        /// </summary>
        private DbSet<DataCheck> _set;

        /// <summary>
        /// Репозиторий проверки данных
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EFDataCheckRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = context.Set<DataCheck>();
        }

        public async Task Delete(Guid dataId)
        {
            var entity = await GetById(dataId);
            if (entity is not null)
                _set.Remove(entity);
        }

        public async Task<DataCheck?> GetById(Guid dataId)
        {
            var entity = await _set.FirstOrDefaultAsync(e => e.DataId == dataId);

            return entity;
        }

        public async Task Insert(DataCheck entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task Update(DataCheck entity)
        {
            await Task.Run(() =>
            {
                _set.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            });
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
