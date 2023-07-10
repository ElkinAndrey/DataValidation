namespace DataValidationAPI.Domain.Constants
{
    /// <summary>
    /// Политики
    /// </summary>
    public class Policies
    {
        /// <summary>
        /// Политика для менеджера и администратора
        /// </summary>
        public static readonly string Manager = "Manager";

        /// <summary>
        /// Политика только для администратора
        /// </summary>
        public static readonly string Admin = "Admin";
    }
}
