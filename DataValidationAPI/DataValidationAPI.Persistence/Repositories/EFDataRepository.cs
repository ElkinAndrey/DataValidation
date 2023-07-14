using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    public class EFDataRepository : EFGenericRepository<Data>, IDataRepository
    {
        private ApplicationDbContext _context;
        private DbSet<Data> _set;

        public EFDataRepository(ApplicationDbContext context) 
            : base(context)
        {
            _context = context;
            _set = context.Set<Data>();
        }

        public async Task<IQueryable<Data>> Get(
            int start = 0,
            int length = int.MaxValue,
            bool onlyValid = false,
            string? email = null,
            DateTime? dateStart = null,
            DateTime? dateEnd = null)
        {

            var data = _set
                .Include(d => d.DataCheck)
                    .ThenInclude(d => d.User)
                .Include(d => d.PersonProvided)
                .Where(d =>
                    !onlyValid || d.DataCheck != null)
                .Where(d =>
                    email == null || email == ""
                    ? true
                    : d.PersonProvided.Email.Contains(email))
                .Where(d =>
                    dateStart == null
                    ? true
                    : d.Date >= dateStart)
                .Where(d =>
                    dateEnd == null
                    ? true
                    : d.Date <= dateEnd)
                .OrderBy(d => d.Date)
                .Skip(start)
                .Take(length);

            return data;
        }
    }
}
