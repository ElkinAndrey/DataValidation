namespace DataValidationAPI.Infrastructure.Dto.User
{
    /// <summary>
    /// Данные для изменения пароля
    /// </summary>
    /// <param name="NewPassword">Новый пароль</param>
    public record class ChangePasswordDto(string? NewPassword);
}
