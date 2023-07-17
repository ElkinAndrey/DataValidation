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

        /// <summary>
        /// Ошибка, возникающая если пользователь не найден
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        public UserNotFoundException(Guid userId) :
            base($"Пользователь с Id \"{userId}\" не найден")
        {

        }
    }
}
