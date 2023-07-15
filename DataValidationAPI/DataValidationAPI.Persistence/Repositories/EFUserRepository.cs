using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    public class EFUserRepository : EFGenericRepository<User>, IUserRepository
    {
        private ApplicationDbContext _context;
        private DbSet<User> _set;

        public EFUserRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
            _set = context.Set<User>();
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = await _set
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<User?> GetByToken(string token)
        {
            var user = await _set
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.RefreshToken == token);

            return user;
        }
    }
}
