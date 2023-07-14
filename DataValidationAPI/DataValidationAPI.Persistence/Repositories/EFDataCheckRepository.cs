using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    /// <summary>
    /// Репозиторий проверки данных
    /// </summary>
    public class EFDataCheckRepository : IDataCheckRepository
    {
        private ApplicationDbContext _context;
        private DbSet<DataCheck> _set;

        public EFDataCheckRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = context.Set<DataCheck>();
        }

        public async Task Delete(Guid dataId)
        {
            var entity = await GetById(dataId);
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
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
