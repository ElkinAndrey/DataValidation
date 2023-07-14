using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace DataValidationAPI.Service.Features
{
    /// <summary>
    /// Работа с хэшами пароля
    /// </summary>
    public static class PasswordHash
    {
        /// <summary>
        /// Получить хэш
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="passwordSalt">Соль</param>
        /// <returns>Хэш</returns>
        private static string GetHash(string password, byte[] passwordSalt)
            => Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: passwordSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

        /// <summary>
        /// Создать хэш
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="passwordHash">Хэш</param>
        /// <param name="passwordSalt">Соль</param>
        public static void Create(string password, out string passwordHash, out byte[] passwordSalt)
        {
            passwordSalt = RandomNumberGenerator.GetBytes(128 / 8);
            passwordHash = GetHash(password, passwordSalt);
        }

        /// <summary>
        /// Проверить пароль
        /// </summary>
        /// <param name="password">пароль</param>
        /// <param name="passwordHash">хэш, с которым нужно сравнить пароль</param>
        /// <param name="passwordSalt">соль</param>
        /// <returns>True - пароль верный, False - пароль не верный</returns>
        public static bool Verify(string password, string passwordHash, byte[] passwordSalt)
            => passwordHash == GetHash(password, passwordSalt);
    }
}
