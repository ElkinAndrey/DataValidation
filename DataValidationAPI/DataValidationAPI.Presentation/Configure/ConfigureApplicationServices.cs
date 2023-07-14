using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Repositories;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DataValidationAPI.Persistence.Configure
{
    public class ConfigureApplicationServices
    {
        private readonly WebApplicationBuilder _builder;

        public ConfigureApplicationServices(WebApplicationBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Настройки базы данных
        /// </summary>
        public void DataBase()
        {
            _builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection"))
            );
        }

        /// <summary>
        /// Настройки сериализации в Json
        /// </summary>
        public void JsonSerializableSettings()
        {
            _builder.Services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        /// <summary>
        /// Внедрение зависимостей
        /// </summary>
        public void DependencyInjection()
        {
            _builder.Services.AddTransient<IGenericRepository<Data>, EFGenericRepository<Data>>();
            _builder.Services.AddTransient<IGenericRepository<Role>, EFGenericRepository<Role>>();
            _builder.Services.AddTransient<IGenericRepository<User>, EFGenericRepository<User>>();

            _builder.Services.AddTransient<IDataRepository, EFDataRepository>();
            _builder.Services.AddTransient<IDataCheckRepository, EFDataCheckRepository>();

            _builder.Services.AddTransient<IDataService, DataService>();
        }

        /// <summary>
        /// Выполнить всё
        /// </summary>
        public void Start()
        {
            DataBase();
            JsonSerializableSettings();
            DependencyInjection();
        }
    }
}
