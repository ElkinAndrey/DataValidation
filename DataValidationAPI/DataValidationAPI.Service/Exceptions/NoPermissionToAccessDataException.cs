namespace DataValidationAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если нет разрешения на доступ к данным
    /// </summary>
    public sealed class NoPermissionToAccessDataException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если нет разрешения на доступ к данным
        /// </summary>
        public NoPermissionToAccessDataException() :
            base($"У вас нет разрешения на доступ к данным")
        {

        }
    }
}
