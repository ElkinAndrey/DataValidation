using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Service.Dto
{
    /// <summary>
    /// Параметры пользователя
    /// </summary>
    /// <remarks>
    /// <para>
    /// Параметр userParam отвечает за то, чтобы пользователь мог видеть свои данные. Если userParam равен null, то на 
    /// принадлежность данных пользователю не будет обращаться внимание. 
    /// UserId - это Id пользователя, данные которого будут получены. Role - роль, которая нужна для того, чтобы 
    /// простой пользователь не мог смотреть не свои данные. TakeOnlyThisUser говорит о том, нужно ли пользователю
    /// взять только свои данные или нужно посмотреть чужие.
    /// </para>
    /// </remarks>
    public class UserParam
    {
        /// <summary>
        /// Параметры пользователя
        /// </summary>
        /// <remarks>
        /// <para>
        /// Параметр userParam отвечает за то, чтобы пользователь мог видеть свои данные. Если userParam равен null, то на 
        /// принадлежность данных пользователю не будет обращаться внимание. 
        /// UserId - это Id пользователя, данные которого будут получены. Role - роль, которая нужна для того, чтобы 
        /// простой пользователь не мог смотреть не свои данные. TakeOnlyThisUser говорит о том, нужно ли пользователю
        /// взять только свои данные или нужно посмотреть чужие.
        /// </para>
        /// </remarks>
        public UserParam() { }

        /// <summary>
        /// UserId - Id человека
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Id получателя данных
        /// </summary>
        public Guid RecipientDataId { get; set; }

        /// <summary>
        /// Role - название роли получателя данные
        /// </summary>
        public string? RoleRecipientData { get; set; } = null;

        /// <summary>
        /// TakeOnlyThisUser - взять ли только данный 
        /// </summary>
        public bool TakeOnlyThisUser { get; set; } = false;
    }
}
