﻿namespace DataValidationAPI.Infrastructure.Dto.User
{
    /// <summary>
    /// Данные для изменения пользователя
    /// </summary>
    /// <remarks>
    /// string? Email,
    /// string? Password
    /// </remarks>
    /// <param name="Email">Новая электронная почта</param>
    /// <param name="Password">Новый пароль</param>
    public record class ChangeUserDto(
        string? Email,
        string? Password);
}
