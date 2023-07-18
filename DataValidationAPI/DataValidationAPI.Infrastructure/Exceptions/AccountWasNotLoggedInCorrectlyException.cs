namespace DataValidationAPI.Infrastructure.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если вход в аккаунт выполнен некорректно
    /// </summary>
    public class AccountWasNotLoggedInCorrectlyException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если вход в аккаунт выполнен некорректно
        /// </summary>
        public AccountWasNotLoggedInCorrectlyException() :
            base($"Вход в аккаунт выполнен некорректно, не удалось получить данные пользователя")
        {

        }
    }
}
