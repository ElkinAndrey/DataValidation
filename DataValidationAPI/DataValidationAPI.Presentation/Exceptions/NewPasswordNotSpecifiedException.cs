namespace DataValidationAPI.Presentation.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если новый пароль не указан
    /// </summary>
    public class NewPasswordNotSpecifiedException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если новый пароль не указан
        /// </summary>
        public NewPasswordNotSpecifiedException() :
            base($"Новый пароль не указан")
        {

        }
    }
}
