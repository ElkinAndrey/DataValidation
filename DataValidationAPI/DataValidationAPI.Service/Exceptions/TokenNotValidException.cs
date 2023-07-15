namespace DataValidationAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если токен не действителен
    /// </summary>
    public sealed class TokenNotValidException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если токен не действителен
        /// </summary>
        public TokenNotValidException() :
            base($"Вы давно не заходили на аккаунт, токен не действителен")
        {

        }

        /// <summary>
        /// Ошибка, возникающая если токен не действителен
        /// </summary>
        public TokenNotValidException(string token) :
            base($"Вы давно не заходили на аккаунт, токен \"{token}\" не действителен")
        {

        }
    }
}
