using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    public class EFRoleRepository : EFGenericRepository<Role>, IRoleRepository
    {
        private ApplicationDbContext _context;
        private DbSet<Role> _set;

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
