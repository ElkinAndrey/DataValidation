namespace DataValidationAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если пользователя с таким Email нет
    /// </summary>
    public sealed class EmailNotFoundException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если пользователя с таким Email нет
        /// </summary>
        public EmailNotFoundException() :
            base($"Введенная электронная почта не найдена")
        {

        }
        /// <summary>
        /// Ошибка, возникающая если пользователя с таким Email нет
        /// </summary>
        /// <param name="email">Электронная почта, которой нет</param>
        public EmailNotFoundException(string email) :
            base($"Электронная почта \"{email}\" не найдена")
        {

        }
    }
}
