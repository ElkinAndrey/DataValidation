namespace DataValidationAPI.Infrastructure.Dto.Data
{
    /// <summary>
    /// Данные для получения данных
    /// </summary>
    /// <param name="Start">Начало отчета</param>
    /// <param name="Length">Длина среза</param>
    /// <param name="Email">Часть электронной почты</param>
    /// <param name="DateStart">Дата начала отчета</param>
    /// <param name="DateEnd">Дата окончания отчета</param>
    public record class GetDatasDto(
        int? Start,
        int? Length,
        string? Email,
        DateTime? DateStart,
        DateTime? DateEnd);
}
