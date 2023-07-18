using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Dto;
using DataValidationAPI.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Repositories
{
    /// <summary>
    /// Репозиторий для работы с данными
    /// </summary>
    public class EFDataRepository : EFGenericRepository<Data>, IDataRepository
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Нужная таблица
        /// </summary>
        private DbSet<Data> _set;

        /// <summary>
        /// Репозиторий для работы с данными
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EFDataRepository(ApplicationDbContext context) 
            : base(context)
        {
            _context = context;
            _set = context.Set<Data>();
        }

        public async Task<IEnumerable<Data>> Get(GetDataFromRepositoryParams param)
        {
            return await Task.Run(() =>
            {
                var data = _set
                    // Подтягивание данных
                    .Include(d => d.DataCheck)
                        .ThenInclude(d => d!.User)
                            .ThenInclude(d => d!.Role)
                    .Include(d => d.PersonProvided)
                        .ThenInclude(d => d!.Role)
                    // Проверка простых данных
                    .Where(d =>
                        param.Email == null || param.Email == ""
                        ? true
                        : d.PersonProvided!.Email!.Contains(param.Email))
                    .Where(d =>
                        param.DateStart == null
                        ? true
                        : d.Date >= param.DateStart)
                    .Where(d =>
                        param.DateEnd == null
                        ? true
                        : d.Date <= param.DateEnd)
                    // Проверки на валидности данных
                    .Where(
                        d => (
                            param.UserParam != null
                            && param.UserParam.UserId == d.PersonProvidedId
                            && (param.UserParam.IsUnverifiedData && d.DataCheck == null
                                || param.UserParam.IsValidatedData && d.DataCheck != null && d.DataCheck.Valid == true
                                || param.UserParam.IsNoValidatedData && d.DataCheck != null && d.DataCheck.Valid == false
                                || param.UserParam.IsCheckData && d.DataCheck != null && d.DataCheck.Valid == null
                            )
                        ) || (
                            (param.UserParam == null || !param.UserParam.TakeOnlyThisUser) 
                            && (param.IsUnverifiedData && d.DataCheck == null
                                || param.IsValidatedData && d.DataCheck != null && d.DataCheck.Valid == true
                                || param.IsNoValidatedData && d.DataCheck != null && d.DataCheck.Valid == false
                                || param.IsCheckData && d.DataCheck != null && d.DataCheck.Valid == null
                            )
                        )
                    )
                    // Выборка нужного
                    .OrderByDescending(d => d.Date)
                    .Skip(param.Start)
                    .Take(param.Length);


                return data;
            });
        }

        public override async Task<Data> GetById(Guid id)
        {
            var entity = await _set
                .Include(d => d.DataCheck)
                    .ThenInclude(d => d!.User)
                        .ThenInclude(d => d!.Role)
                .Include(d => d.PersonProvided)
                    .ThenInclude(d => d!.Role)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity is null)
                throw new EntityNotFoundException(id);

            return entity;
        }
    }
}
