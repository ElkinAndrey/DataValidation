namespace DataValidationAPI.Presentation.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если токена обновления нет в куки
    /// </summary>
    public class RefreshTokenNotInCookieException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если токена обновления нет в куки
        /// </summary>
        public RefreshTokenNotInCookieException() :
            base($"Токена обновления нет в куки")
        {

        }
    }
}
