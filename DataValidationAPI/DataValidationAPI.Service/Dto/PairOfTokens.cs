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
    }
}
