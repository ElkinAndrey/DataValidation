namespace DataValidationAPI.Infrastructure.Dto.ProfileData
{
    /// <summary>
    /// Данные для получения количества своих данных
    /// </summary>
    /// <remarks>
    /// DateTime? DateStart,
    /// DateTime? DateEnd,
    /// bool? IsUnverifiedData = true,
    /// bool? IsValidatedData = true,
    /// bool? IsNoValidatedData = true,
    /// bool? IsCheckData = true);
    /// </remarks>
    /// <param name="DateStart">Дата начала отчета</param>
    /// <param name="DateEnd">Дата окончания отчета</param>
    /// <param name="IsUnverifiedData">Взять не проверенные</param>
    /// <param name="IsValidatedData">Взять прошедшие проверку</param>
    /// <param name="IsNoValidatedData">Взять не прошедшие проверку</param>
    /// <param name="IsCheckData">Взять находящиеся на проверке</param>
    public record class GetYourDataCountDto(
        DateTime? DateStart,
        DateTime? DateEnd,
        bool? IsUnverifiedData = true,
        bool? IsValidatedData = true,
        bool? IsNoValidatedData = true,
        bool? IsCheckData = true);
}
