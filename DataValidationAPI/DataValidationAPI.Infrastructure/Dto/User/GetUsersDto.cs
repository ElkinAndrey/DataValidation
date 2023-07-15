namespace DataValidationAPI.Infrastructure.Dto.User
{
    /// <summary>
    /// Данные для получения пользователей 
    /// </summary>
    /// <param name="Email">Часть электронная почта</param>
    /// <param name="RoleId">Id роли</param>
    public record class GetUsersDto(
        string? Email,
        Guid? RoleId);

    /// <summary>
    /// Данные для изменения пользователя
    /// </summary>
    /// <param name="Email">Новая электронная почта</param>
    /// <param name="Password">Новый пароль</param>
    public record class ChangeUserDto(
        string? Email,
        string? Password);
}
