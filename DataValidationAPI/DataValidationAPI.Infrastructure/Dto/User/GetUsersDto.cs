namespace DataValidationAPI.Infrastructure.Dto.User
{
    /// <summary>
    /// Данные для получения пользователей 
    /// </summary>
    /// <param name="Start">Начало отчета</param>
    /// <param name="Length">Длина среза</param>
    /// <param name="Email">Часть электронная почта</param>
    /// <param name="RoleId">Id роли</param>
    /// <param name="StartRegistrationDate">Начало отчета даты регистрации</param>
    /// <param name="EndRegistrationDate">Конец отчета даты регистрации</param>
    public record class GetUsersDto(
        int? Start,
        int? Length,
        string? Email,
        Guid? RoleId,
        DateTime? StartRegistrationDate,
        DateTime? EndRegistrationDate);
}
