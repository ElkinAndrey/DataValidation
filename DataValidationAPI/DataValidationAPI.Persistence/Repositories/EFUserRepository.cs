using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями
    /// </summary>
    public class EFUserRepository : EFGenericRepository<User>, IUserRepository
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Нужная таблица
        /// </summary>
        private DbSet<User> _set;

        /// <summary>
        /// Репозиторий для работы с пользователями
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
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
            return await Task.Run(() =>
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
            });
        }

        public override async Task<User> GetById(Guid id)
        {
            var user = await _set
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new EntityNotFoundException();

            return user;
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
