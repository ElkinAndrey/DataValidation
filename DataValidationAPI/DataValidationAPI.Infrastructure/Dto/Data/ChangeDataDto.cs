namespace DataValidationAPI.Infrastructure.Dto.Data
{
    /// <summary>
    /// Изменение данных
    /// </summary>
    /// <remarks>
    /// string? Information,
    /// Guid? PersonProvidedId
    /// </remarks>
    /// <param name="Information">Информация</param>
    /// <param name="PersonProvidedId">Человек, вносящий данные</param>
    public record class ChangeDataDto(
        string? Information,
        Guid? PersonProvidedId);
}
