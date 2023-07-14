namespace DataValidationAPI.Domain.Constants
{
    public static class JwtLifetime
    {
        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        public static TimeSpan AccessTimeSpan { get; } = new TimeSpan(
            days: 1,
            hours: 0,
            minutes: 0,
            seconds: 0);

        /// <summary>
        /// Время жизни токена обновления
        /// </summary>
        public static TimeSpan RefreshTimeSpan { get; } = new TimeSpan(
            days: 60,
            hours: 0,
            minutes: 0,
            seconds: 0);
    }
}
