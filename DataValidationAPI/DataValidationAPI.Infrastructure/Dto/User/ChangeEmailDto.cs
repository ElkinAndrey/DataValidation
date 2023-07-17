namespace DataValidationAPI.Infrastructure.Dto.User
{
    /// <summary>
    /// Данные для изменения электронной почты
    /// </summary>
    /// <param name="NewEmail">Новая электронная почта</param>
    public record class ChangeEmailDto(string? NewEmail);
}
