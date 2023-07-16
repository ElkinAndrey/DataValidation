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

        public async Task<IQueryable<User>> Get(
            int start = 0,
            int length = int.MaxValue,
            string? email = null,
            Guid? roleId = null,
            DateTime? startRegistrationDate = null,
            DateTime? endRegistrationDate = null)
        {
            var users = _set
                .Include(u => u.Role)
                .Where(u =>
                    email == null || email == ""
                    ? true
                    : u.Email.Contains(email))
                .Where(u =>
                    roleId == null
                    ? true
                    : u.RoleId == roleId)
                .Where(d =>
                    startRegistrationDate == null
                    ? true
                    : d.RegistrationDate >= startRegistrationDate)
                .Where(d =>
                    endRegistrationDate == null
                    ? true
                    : d.RegistrationDate <= endRegistrationDate)
                .OrderByDescending(u => u.RegistrationDate)
                .Skip(start)
                .Take(length);

            return users;
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
