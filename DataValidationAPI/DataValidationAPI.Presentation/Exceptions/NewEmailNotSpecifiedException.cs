namespace DataValidationAPI.Presentation.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если новая электроннная почта не указана
    /// </summary>
    public class NewEmailNotSpecifiedException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если новая электроннная почта не указана
        /// </summary>
        public NewEmailNotSpecifiedException() :
            base($"Новая электронная почта не указана")
        {

        }
    }
}
