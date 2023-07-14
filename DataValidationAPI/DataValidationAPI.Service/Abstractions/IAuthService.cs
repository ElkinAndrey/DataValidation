using DataValidationAPI.Service.Dto;

namespace DataValidationAPI.Service.Abstractions
{
    public interface IAuthService
    {
        /// <summary>
        /// Зарегистрировать человека
        /// </summary>
        /// <param name="email">Электронная почта</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public Task<PairOfTokens> RegisterAsync(
            string email,
            string password,
            string secretKey);

        /// <summary>
        /// Войти в аккаунт
        /// </summary>
        /// <param name="email">Электронная почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="secretKey">Секретный ключ для формирования Access токена</param>
        /// <returns>AccessToken - токен доступа, RefreshToken - токен обновления</returns>
        public Task<PairOfTokens> LoginAsync(
            string email,
            string password,
            string secretKey);

        /// <summary>
        /// Удалить токен из базы данных
        /// </summary>
        /// <param name="token">Токен</param>
        /// <returns></returns>
        public Task DeleteTokenAsync(string token);

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="secretKey">Секретный ключ для формирования Access токена</param>
        /// <returns>AccessToken - токен доступа, RefreshToken - токен обновления</returns>
        public Task<PairOfTokens> RefreshTokenAsync(
            string token,
            string secretKey);
    }
}
