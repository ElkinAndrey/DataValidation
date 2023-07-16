namespace DataValidationAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если аккаунт не активен
    /// </summary>
    public sealed class AccountNotActiveException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если аккаунт не активен
        /// </summary>
        public AccountNotActiveException() :
            base($"Аккаунт не активен")
        {

        }
        /// <summary>
        /// Ошибка, возникающая если аккаунт не активен
        /// </summary>
        /// <param name="email">Электронная почта</param>
        public AccountNotActiveException(string email) :
            base($"Аккаунт \"{email}\" не активен")
        {

        }
    }
}
