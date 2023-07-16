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

        /// <summary>
        /// Список пользователей с такой ролью
        /// </summary>
        public List<User> Users { get; set; } = new List<User>();
    }
}
