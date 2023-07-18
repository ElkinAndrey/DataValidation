namespace DataValidationAPI.Infrastructure.Dto.User
{
    /// <summary>
    /// Данные для изменения пароля
    /// </summary>
    /// <remarks>
    /// string? NewPassword
    /// </remarks>
    /// <param name="NewPassword">Новый пароль</param>
    public record class ChangePasswordDto(string? NewPassword);
}
