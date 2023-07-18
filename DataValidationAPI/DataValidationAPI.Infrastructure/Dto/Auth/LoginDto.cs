namespace DataValidationAPI.Infrastructure.Dto.Auth
{
    /// <summary>
    /// Данные для входа
    /// </summary>
    /// <remarks>
    /// string Email
    /// string Password
    /// </remarks>
    /// <param name="Email">Электронная почта</param>
    /// <param name="Password">Пароль</param>
    public record class LoginDto(
        string Email,
        string Password);
}
