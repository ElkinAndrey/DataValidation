using DataValidationAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.Entity<Role>()
                .HasAlternateKey(r => r.Name);

            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Email);

            modelBuilder.Entity<DataCheck>()
                .HasKey(p => new { p.UserId, p.DataId });

            modelBuilder.Entity<DataCheck>()
                .HasOne(p => p.Data)
                .WithOne(t => t.DataCheck)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
