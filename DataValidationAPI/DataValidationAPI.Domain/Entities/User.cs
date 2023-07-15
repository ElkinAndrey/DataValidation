namespace DataValidationAPI.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Роль
        /// </summary>
        public required Role Role { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Соль пароля
        /// </summary>
        public byte[] PasswordSalt { get; set; } = new byte[0];

        /// <summary>
        /// Токен обновления
        /// </summary>
        public string? RefreshToken { get; set; } = null;

        /// <summary>
        /// Дата окончания работы токена обновления
        /// </summary>
        public DateTime? TokenExpirationDate {  get; set; } = null;

        /// <summary>
        /// Проверки данных
        /// </summary>
        public List<DataCheck> DataChecks { get; set; } = new List<DataCheck>();
    }
}
