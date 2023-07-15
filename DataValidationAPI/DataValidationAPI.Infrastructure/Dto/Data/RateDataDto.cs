namespace DataValidationAPI.Infrastructure.Dto.Data
{
    /// <summary>
    /// Данные для оценки данных
    /// </summary>
    /// <param name="Valid">Оценка данных</param>
    public record class RateDataDto(
        bool? Valid);
}
