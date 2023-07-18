using DataValidationAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DataValidationAPI.Persistence
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        #region Данные

        /// <summary>
        /// Данные
        /// </summary>
        public DbSet<Data> Data { get; set; }

        /// <summary>
        /// Проверки данных
        /// </summary>
        public DbSet<DataCheck> DataCheck { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public DbSet<Role> Role { get; set; }

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> User { get; set; }

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options">Настройки</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Конфигурации
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Добавление уникальных атрибутов
            modelBuilder.Entity<Role>()
                .HasAlternateKey(r => r.Name);

            // Добавление двух первичных ключей
            modelBuilder.Entity<DataCheck>()
                .HasKey(p => new { p.UserId, p.DataId });

            // Отключение каскадных удалений
            modelBuilder.Entity<DataCheck>()
                .HasOne(p => p.User)
                .WithMany(t => t.DataChecks)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Data>()
                .HasOne(p => p.PersonProvided)
                .WithMany(t => t.Datas)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<User>()
                .HasOne(p => p.Role)
                .WithMany(t => t.Users)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Добавление ролей в базу данных
            modelBuilder.Entity<Role>().HasData(DataForDB.Roles);

            // Инициализация данных по умолчанию
            CreateData(modelBuilder);
        }

        /// <summary>
        /// Создание данных для БД
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void CreateData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(DataForDB.Users);
            modelBuilder.Entity<Data>().HasData(DataForDB.Datas);
            modelBuilder.Entity<DataCheck>().HasData(DataForDB.DataChecks);
        }
    }
}
