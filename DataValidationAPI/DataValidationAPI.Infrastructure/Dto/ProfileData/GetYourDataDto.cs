namespace DataValidationAPI.Infrastructure.Dto.ProfileData
{
    /// <summary>
    /// Данные для получения своих данных
    /// </summary>
    /// <param name="Start">Начало отчета</param>
    /// <param name="Length">Длина среза</param>
    /// <param name="DateStart">Дата начала отчета</param>
    /// <param name="DateEnd">Дата окончания отчета</param>
    public record class GetYourDataDto(
        int? Start,
        int? Length,
        DateTime? DateStart,
        DateTime? DateEnd);
}
