using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    /// <summary>
    /// Репозиторий для работы с ролями
    /// </summary>
    public class EFRoleRepository : EFGenericRepository<Role>, IRoleRepository
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Нужная таблица
        /// </summary>
        private DbSet<Role> _set;

        /// <summary>
        /// Репозиторий для работы с ролями
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EFRoleRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
            _set = context.Set<Role>();
        }

        public async Task<Role?> GetByName(string name)
        {
            var role = await _set.FirstOrDefaultAsync(u => u.Name == name);
            return role;
        }
    }
}
