namespace DataValidationAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если пользователь не найден
    /// </summary>
    public sealed class UserNotFoundException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если пользователь не найден
        /// </summary>
        public UserNotFoundException() :
            base($"Пользователь не найден")
        {

        }
    }
}
