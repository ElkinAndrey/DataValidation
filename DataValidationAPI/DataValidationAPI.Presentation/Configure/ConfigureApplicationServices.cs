using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataValidationAPI.Persistence.Configure
{
    public class ConfigureApplicationServices
    {
        private readonly WebApplicationBuilder _builder;

        public ConfigureApplicationServices(WebApplicationBuilder builder)
        {
            _builder = builder;
        }

        public void DataBase()
        {
            _builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection"))
            );
        }

        public void DependencyInjection()
        {
            _builder.Services.AddTransient<IGenericRepository<Data>, EFGenericRepository<Data>>();
            _builder.Services.AddTransient<IGenericRepository<Role>, EFGenericRepository<Role>>();
            _builder.Services.AddTransient<IGenericRepository<User>, EFGenericRepository<User>>();
        }

        public void Start()
        {
            DataBase();
            DependencyInjection();
        }
    }
}
