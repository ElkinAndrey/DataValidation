namespace DataValidationAPI.Infrastructure.Dto.Profile
{
    /// <summary>
    /// Изменить свой профиль
    /// </summary>
    /// <param name="email">Новая электронная почта</param>
    /// <param name="password">Новый пароль</param>
    public record class ChangeYourProfileDto(
        string? email,
        string? password);
}
