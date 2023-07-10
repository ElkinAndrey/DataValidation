namespace DataValidationAPI.Domain.Entities
{
    /// <summary>
    /// Роль
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
