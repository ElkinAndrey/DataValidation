namespace DataValidationAPI.Infrastructure.Dto.Data
{
    /// <summary>
    /// Данные для оценки данных
    /// </summary>
    /// <remarks>
    /// bool? Valid
    /// </remarks>
    /// <param name="Valid">Оценка данных</param>
    public record class RateDataDto(
        bool? Valid);
}
