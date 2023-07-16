namespace DataValidationAPI.Persistence.Dto
{
    /// <summary>
    /// Параметры для получения данных
    /// </summary>
    /// <remarks>
    /// <para>
    /// start - начало отчета среза, length - длина среза, email -  часть электронной почты, dateStart - начало 
    /// промежутка даты создания, dateEnd - конец промежутка даты создания
    /// </para>
    /// <para>
    /// Есть данные 4 типов. Не проверенные (isUnverifiedData), прошедшие проверку (isValidatedData), не прошедшие 
    /// проверку (isNoValidatedData) и данные, которые только начали проверять (isCheckData). 
    /// </para>
    /// <para>
    /// Параметр userParam отвечает за то, чтобы пользователь мог видеть свои данные. Если userParam равен null, то на 
    /// принадлежность данных пользователю не будет обращаться внимание. 
    /// UserId - это Id пользователя, данные которого будут получены. Role - роль, которая нужна для того, чтобы 
    /// простой пользователь не мог смотреть не свои данные. TakeOnlyThisUser говорит о том, нужно ли пользователю
    /// взять только свои данные или нужно посмотреть чужие.
    /// </para>
    /// </remarks>
    public class GetDataFromRepositoryParams
    {
        /// <summary>
        /// Параметры для получения данных
        /// </summary>
        /// <remarks>
        /// <para>
        /// start - начало отчета среза, length - длина среза, email -  часть электронной почты, dateStart - начало 
        /// промежутка даты создания, dateEnd - конец промежутка даты создания
        /// </para>
        /// <para>
        /// Есть данные 4 типов. Не проверенные (isUnverifiedData), прошедшие проверку (isValidatedData), не прошедшие 
        /// проверку (isNoValidatedData) и данные, которые только начали проверять (isCheckData). 
        /// </para>
        /// <para>
        /// Параметр userParam отвечает за то, чтобы пользователь мог видеть свои данные. Если userParam равен null, то на 
        /// принадлежность данных пользователю не будет обращаться внимание. 
        /// UserId - это Id пользователя, данные которого будут получены. Role - роль, которая нужна для того, чтобы 
        /// простой пользователь не мог смотреть не свои данные. TakeOnlyThisUser говорит о том, нужно ли пользователю
        /// взять только свои данные или нужно посмотреть чужие.
        /// </para>
        /// </remarks>
        public GetDataFromRepositoryParams() { }

        /// <summary>
        /// Начало отчета
        /// </summary>
        public int Start { get; set; } = 0;

        /// <summary>
        /// Длина среза
        /// </summary>
        public int Length { get; set; } = int.MaxValue;

        /// <summary>
        /// Часть электронной почты
        /// </summary>
        public string? Email { get; set; } = null;

        /// <summary>
        /// Начало промежутка даты создания
        /// </summary>
        public DateTime? DateStart { get; set; } = null;

        /// <summary>
        /// Конец промежутка даты создания
        /// </summary>
        public DateTime? DateEnd { get; set; } = null;

        /// <summary>
        /// Взять не проверенные
        /// </summary>
        public bool IsUnverifiedData { get; set; } = true;
        /// <summary>
        /// Взять прошедшие проверку
        /// </summary>
        public bool IsValidatedData { get; set; } = true;

        /// <summary>
        /// Взять не прошедшие проверку
        /// </summary>
        public bool IsNoValidatedData { get; set; } = true;

        /// <summary>
        /// Взять находящиеся на проверке
        /// </summary>
        public bool IsCheckData { get; set; } = true;

        /// <summary>
        /// UserId - Id человека, TakeOnlyThisUser - взять ли только данный 
        /// </summary>
        public UserParamForRepository? UserParam { get; set; } = null;
    }
}
