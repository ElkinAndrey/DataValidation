namespace DataValidationAPI.Service.Dto
{
    /// <summary>
    /// Пара токена доступа и токена обновления
    /// </summary>
    public class PairOfTokens
    {
        /// <summary>
        /// Токен доступа
        /// </summary>
        public required string AccessToken { get; set; }

        /// <summary>
        /// Токен обновления
        /// </summary>
        public required string RefreshToken { get; set; }

        /// <summary>
        /// Дата генерации токенов
        /// </summary>
        public DateTime GenerationDate { get; set; } = DateTime.Now;
    }
}
