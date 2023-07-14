namespace DataValidationAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если пользователь с таким Email уже есть
    /// </summary>
    public sealed class EmailAlreadyExistsException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если пользователь с таким Email уже есть
        /// </summary>
        public EmailAlreadyExistsException() :
            base($"Введенная электронная почта уже используется на другом аккаунте")
        {

        }
        /// <summary>
        /// Ошибка, возникающая если пользователь с таким Email уже есть
        /// </summary>
        /// <param name="email">Электронная почта, которая уже есть</param>
        public EmailAlreadyExistsException(string email) :
            base($"Электронная почта \"{email}\" уже используется на другом аккаунте")
        {

        }
    }
}
