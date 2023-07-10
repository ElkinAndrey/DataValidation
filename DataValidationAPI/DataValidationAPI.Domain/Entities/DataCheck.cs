namespace DataValidationAPI.Domain.Entities
{
    /// <summary>
    /// Проверка данных
    /// </summary>
    public class DataCheck : BaseEntity
    {
        /// <summary>
        /// Данные
        /// </summary>
        public required Data Data { get; set; }

        /// <summary>
        /// Проверяющий
        /// </summary>
        public required User User { get; set; }

        /// <summary>
        /// Прошло проверку или нет
        /// </summary>
        public bool? Valid { get; set; } = null;
    }
}
