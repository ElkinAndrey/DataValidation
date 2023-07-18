using DataValidationAPI.Infrastructure.Exceptions;
using DataValidationAPI.Persistence.Exceptions;
using DataValidationAPI.Service.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DataValidationAPI.Infrastructure.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "text/plain;charset=utf-8";

            /// 400 - некорректный, не полный или пустой запрос (например, если в 
            ///       JSON не был передан какой то параметр)
            /// 401 - клиент не авторизован
            /// 403 - утрачено разрешение доступа
            /// 404 - страница или элемент не найден (например, если неверно 
            ///       указан ID)
            /// 409 - конфликт при выполнении запроса (например, если человека 
            ///       пытаются добавить в клуб, а он уже состоит в клубе или 
            ///       пользователь уже зарегистрирован по такой почте)   
            /// 422 - ошибка валидации (неверный формат введения Email или пароль 
            ///       слишком простой)

            if (exception is NewEmailNotSpecifiedException)
                return CreateError(context, 400, exception);
            if (exception is NewPasswordNotSpecifiedException)
                return CreateError(context, 400, exception);

            if (exception is RefreshTokenNotInCookieException)
                return CreateError(context, 401, exception);
            if (exception is AccountWasNotLoggedInCorrectlyException)
                return CreateError(context, 401, exception);
            if (exception is WrongPasswordException)
                return CreateError(context, 401, exception);
            if (exception is TokenNotValidException)
                return CreateError(context, 401, exception);
            if (exception is NoPermissionToAccessDataException)
                return CreateError(context, 401, exception);
            if (exception is InsufficientRightsException)
                return CreateError(context, 401, exception);

            if (exception is EntityNotFoundException)
                return CreateError(context, 404, exception);
            if (exception is AccountNotActiveException)
                return CreateError(context, 404, exception);
            if (exception is EmailNotFoundException)
                return CreateError(context, 404, exception);
            if (exception is UserNotFoundException)
                return CreateError(context, 404, exception);

            if (exception is EmailAlreadyExistsException)
                return CreateError(context, 409, exception);

            return CreateError(context, 500, $"Неизвестная ошибка");
        }

        /// <summary>
        /// Создать ошибку
        /// </summary>
        /// <remarks>
        /// Вынос повторяющейся информации
        /// </remarks>
        /// <param name="context">HttpContext</param>
        /// <param name="statusCode">Статус код</param>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        private static Task CreateError(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(message);
        }

        /// <summary>
        /// Создать ошибку
        /// </summary>
        /// <remarks>
        /// Вынос повторяющейся информации
        /// </remarks>
        /// <param name="context">HttpContext</param>
        /// <param name="statusCode">Статус код</param>
        /// <param name="exception">Возникшая ошибка</param>
        /// <returns></returns>
        private static Task CreateError(HttpContext context, int statusCode, Exception exception)
        {
            return CreateError(context, statusCode, exception.Message);
        }
    }
}
