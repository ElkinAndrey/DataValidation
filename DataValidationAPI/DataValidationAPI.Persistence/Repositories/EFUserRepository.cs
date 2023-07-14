using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var user = await _set.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }

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
