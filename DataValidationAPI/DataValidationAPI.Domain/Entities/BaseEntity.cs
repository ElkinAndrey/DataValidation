using System.ComponentModel.DataAnnotations;

namespace DataValidationAPI.Domain.Entities
{
    /// <summary>
    /// Базовая сущность, содержит только Id
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Уникальный Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}
