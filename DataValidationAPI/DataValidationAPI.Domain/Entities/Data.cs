namespace DataValidationAPI.Domain.Entities
{
    /// <summary>
    /// Проверяемые данных
    /// </summary>
    public class Data : BaseEntity
    {
        /// <summary>
        /// Время оставления данных
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Информация
        /// </summary>
        public string Information { get; set; } = string.Empty;

        /// <summary>
        /// Проверка данных
        /// </summary>
        public DataCheck? DataCheck { get; set; } = null;
    }
}
