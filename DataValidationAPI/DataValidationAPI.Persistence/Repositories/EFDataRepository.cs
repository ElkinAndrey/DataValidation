using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Exceptions;
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
            Guid? userId = null,
            string? email = null,
            DateTime? dateStart = null,
            DateTime? dateEnd = null)
        {

            var data = _set
                .Include(d => d.DataCheck)
                    .ThenInclude(d => d.User)
                        .ThenInclude(d => d.Role)
                .Include(d => d.PersonProvided)
                    .ThenInclude(d => d.Role)
                .Where(d =>
                    !onlyValid 
                    || (d.DataCheck != null && d.DataCheck.Valid == true)
                    || (d.PersonProvided.Id == userId))
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
                .OrderByDescending(d => d.Date)
                .Skip(start)
                .Take(length);

            return data;
        }

        public override async Task<Data> GetById(Guid id)
        {
            var entity = await _set
                .Include(d => d.DataCheck)
                    .ThenInclude(d => d.User)
                        .ThenInclude(d => d.Role)
                .Include(d => d.PersonProvided)
                    .ThenInclude(d => d.Role)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity is null)
                throw new EntityNotFoundException(id);

            return entity;
        }
    }
}
