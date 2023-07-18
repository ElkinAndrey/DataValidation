namespace DataValidationAPI.Infrastructure.Dto.ProfileData
{
    /// <summary>
    /// Изменение данных
    /// </summary>
    /// <remarks>
    /// string? Information
    /// </remarks>
    /// <param name="Information">Информация</param>
    public record class ChangeYourDataDto(
        string? Information);
}
