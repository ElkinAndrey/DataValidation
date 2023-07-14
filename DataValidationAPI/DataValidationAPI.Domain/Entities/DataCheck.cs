namespace DataValidationAPI.Domain.Entities
{
    /// <summary>
    /// Проверка данных
    /// </summary>
    public class DataCheck
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Id данных
        /// </summary>
        public Guid DataId { get; set; }

        /// <summary>
        /// Данные
        /// </summary>
        public Data Data { get; set; }

        /// <summary>
        /// Проверяющий
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Прошло проверку или нет
        /// </summary>
        public bool? Valid { get; set; } = null;
    }
}
