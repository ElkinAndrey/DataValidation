namespace DataValidationAPI.Infrastructure.Dto.Data
{
    /// <summary>
    /// Данные для получения данных
    /// </summary>
    /// <remarks>
    /// int? Start,
    /// int? Length,
    /// string? Email,
    /// DateTime? DateStart,
    /// DateTime? DateEnd,
    /// bool? IsUnverifiedData = true,
    /// bool? IsValidatedData = true,
    /// bool? IsNoValidatedData = true,
    /// bool? IsCheckData = true
    /// </remarks>
    /// <param name="Start">Начало отчета</param>
    /// <param name="Length">Длина среза</param>
    /// <param name="Email">Часть электронной почты</param>
    /// <param name="DateStart">Дата начала отчета</param>
    /// <param name="DateEnd">Дата окончания отчета</param>
    /// <param name="IsUnverifiedData">Взять не проверенные</param>
    /// <param name="IsValidatedData">Взять прошедшие проверку</param>
    /// <param name="IsNoValidatedData">Взять не прошедшие проверку</param>
    /// <param name="IsCheckData">Взять находящиеся на проверке</param>
    public record class GetDatasDto(
        int? Start,
        int? Length,
        string? Email,
        DateTime? DateStart,
        DateTime? DateEnd,
        bool? IsUnverifiedData = true,
        bool? IsValidatedData = true,
        bool? IsNoValidatedData = true,
        bool? IsCheckData = true);

    /// <summary>
    /// Данные для получения количества данных
    /// </summary>
    /// <remarks>
    /// string? Email,
    /// DateTime? DateStart,
    /// DateTime? DateEnd,
    /// bool? IsUnverifiedData = true,
    /// bool? IsValidatedData = true,
    /// bool? IsNoValidatedData = true,
    /// bool? IsCheckData = true
    /// </remarks>
    /// <param name="Email">Часть электронной почты</param>
    /// <param name="DateStart">Дата начала отчета</param>
    /// <param name="DateEnd">Дата окончания отчета</param>
    /// <param name="IsUnverifiedData">Взять не проверенные</param>
    /// <param name="IsValidatedData">Взять прошедшие проверку</param>
    /// <param name="IsNoValidatedData">Взять не прошедшие проверку</param>
    /// <param name="IsCheckData">Взять находящиеся на проверке</param>
    public record class GetDataCountDto(
        string? Email,
        DateTime? DateStart,
        DateTime? DateEnd,
        bool? IsUnverifiedData = true,
        bool? IsValidatedData = true,
        bool? IsNoValidatedData = true,
        bool? IsCheckData = true);
}