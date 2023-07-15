namespace DataValidationAPI.Infrastructure.Dto.Auth
{
    /// <summary>
    /// Данные для входа
    /// </summary>
    /// <param name="Email">Электронная почта</param>
    /// <param name="Password">Пароль</param>
    public record class LoginDto(
        string Email,
        string Password);
}
