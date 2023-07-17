namespace DataValidationAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если у пользователя не достаточно прав на выполнение действия
    /// </summary>
    public sealed class InsufficientRightsException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если у пользователя не достаточно прав на выполнение действия
        /// </summary>
        public InsufficientRightsException() :
            base($"У вас недостаточно прав на выполнение действия")
        {

        }
    }
}
