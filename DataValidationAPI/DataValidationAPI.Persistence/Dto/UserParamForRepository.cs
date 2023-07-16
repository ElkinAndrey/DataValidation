namespace DataValidationAPI.Persistence.Dto
{
    /// <summary>
    /// Параметры пользователя
    /// </summary>
    /// <remarks>
    /// <para>
    /// Параметр userParam отвечает за то, чтобы пользователь мог видеть свои данные. Если userParam равен null, то на 
    /// принадлежность данных пользователю не будет обращаться внимание. 
    /// UserId - это Id пользователя, данные которого будут получены. TakeOnlyThisUser говорит о том, нужно ли пользователю
    /// взять только свои данные или нужно посмотреть чужие.
    /// </para>
    /// </remarks>
    public class UserParamForRepository
    {
        /// <summary>
        /// Параметры пользователя
        /// </summary>
        /// <remarks>
        /// <para>
        /// Параметр userParam отвечает за то, чтобы пользователь мог видеть свои данные. Если userParam равен null, то на 
        /// принадлежность данных пользователю не будет обращаться внимание. 
        /// UserId - это Id пользователя, данные которого будут получены. TakeOnlyThisUser говорит о том, нужно ли пользователю
        /// взять только свои данные или нужно посмотреть чужие.
        /// </para>
        /// </remarks>
        public UserParamForRepository() { }

        /// <summary>
        /// UserId - Id человека
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// TakeOnlyThisUser - взять ли только данный 
        /// </summary>
        public bool TakeOnlyThisUser { get; set; } = false;

        /// <summary>
        /// Взять не проверенные данные у человека
        /// </summary>
        public bool IsUnverifiedData { get; set; } = true;
        /// <summary>
        /// Взять прошедшие проверку данные у человека
        /// </summary>
        public bool IsValidatedData { get; set; } = true;

        /// <summary>
        /// Взять не прошедшие проверку данные у человека
        /// </summary>
        public bool IsNoValidatedData { get; set; } = true;

        /// <summary>
        /// Взять находящиеся на проверке данные у человека
        /// </summary>
        public bool IsCheckData { get; set; } = true;
    }
}
