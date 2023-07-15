namespace DataValidationAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если введен неверный пароль
    /// </summary>
    public sealed class WrongPasswordException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если введен неверный пароль
        /// </summary>
        public WrongPasswordException() :
            base($"Введен неверный пароль")
        {

        }
        /// <summary>
        /// Ошибка, возникающая если введен неверный пароль
        /// </summary>
        /// <param name="password">Пароль</param>
        public WrongPasswordException(string password) :
            base($"Введен неверный пароль \"{password}\"")
        {

        }
    }
}
