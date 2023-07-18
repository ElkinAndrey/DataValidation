namespace DataValidationAPI.Infrastructure.Dto.User
{
    /// <summary>
    /// Данные для изменения электронной почты
    /// </summary>
    /// <remarks>
    /// string? NewEmail
    /// </remarks>
    /// <param name="NewEmail">Новая электронная почта</param>
    public record class ChangeEmailDto(string? NewEmail);
}
