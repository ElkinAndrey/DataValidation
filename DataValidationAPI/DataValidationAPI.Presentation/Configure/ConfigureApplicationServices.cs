﻿using DataValidationAPI.Domain.Constants;
using DataValidationAPI.Domain.Entities;
using DataValidationAPI.Persistence.Abstractions;
using DataValidationAPI.Persistence.Repositories;
using DataValidationAPI.Service.Abstractions;
using DataValidationAPI.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;

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
        /// Настройки Swagger
        /// </summary>
        public void SwaggerSettings()
        {
            // Для авторизации
            _builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Стандартный заголовок авторизации с использованием схемы Bearer (\"bearer {токен}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        /// <summary>
        /// Настройки аутентификации
        /// </summary>
        public void AuthenticationSettings()
        {
            _builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(_builder.Configuration.GetSection("AppSettings:Token").Value!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero, // Для нормальной работы времени в токене
                    };
                });
        }

        /// <summary>
        /// Политики для авторизации
        /// </summary>
        public void Policy()
        {
            _builder.Services.AddAuthorization(options =>
            {
                // Менеджер
                options.AddPolicy(Policies.Manager, builder =>
                {
                    builder.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Roles.Manager)
                                               || x.User.HasClaim(ClaimTypes.Role, Roles.Admin));
                });

                // Администратор
                options.AddPolicy(Policies.Admin, builder =>
                {
                    builder.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Roles.Admin));
                });
            });
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
            _builder.Services.AddTransient<IUserRepository, EFUserRepository>();
            _builder.Services.AddTransient<IRoleRepository, EFRoleRepository>();

            _builder.Services.AddTransient<IDataService, DataService>();
            _builder.Services.AddTransient<IAuthService, AuthService>();
        }

        /// <summary>
        /// Выполнить всё
        /// </summary>
        public void Start()
        {
            DataBase();
            SwaggerSettings();
            AuthenticationSettings();
            Policy();
            JsonSerializableSettings();
            DependencyInjection();
        }
    }
}