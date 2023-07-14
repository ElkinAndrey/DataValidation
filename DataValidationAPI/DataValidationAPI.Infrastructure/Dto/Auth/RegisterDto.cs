namespace DataValidationAPI.Infrastructure.Dto.Auth
{
    /// <summary>
    /// Данные для регистрации
    /// </summary>
    /// <param name="Email">Электронная почта</param>
    /// <param name="Password">Пароль</param>
    public record class RegisterDto(
        string Email,
        string Password);
}
