namespace DataValidationAPI.Persistence.Exceptions
{
    /// <summary>
    /// Ошибка, возникающая если сущность не найдена
    /// </summary>
    public sealed class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Ошибка, возникающая если сущность не найдена
        /// </summary>
        public EntityNotFoundException() 
            : base($"Сущность не найдена") { }

        /// <summary>
        /// Ошибка, возникающая если сущность не найдена
        /// </summary>
        /// <param name="id">Id сущности</param>
        public EntityNotFoundException(string id)
            : base($"Сущность с id \"{id}\" не найдена") { }

        /// <summary>
        /// Ошибка, возникающая если сущность не найдена
        /// </summary>
        /// <param name="id">Id сущности</param>
        public EntityNotFoundException(Guid id) 
            : this(id.ToString()) { }

        /// <summary>
        /// Ошибка, возникающая если сущность не найдена
        /// </summary>
        /// <param name="type">Тип сущности</param>
        public EntityNotFoundException(Type type)
            : base($"Сущность \"{type}\" не найдена") { }

        /// <summary>
        /// Ошибка, возникающая если сущность не найдена
        /// </summary>
        /// <param name="type">Тип сущности</param>
        /// <param name="id">Id сущности</param>
        public EntityNotFoundException(string type, string id)
            : base($"Сущность \"{type}\" с id \"{id}\" не найдена") { }

        /// <summary>
        /// Ошибка, возникающая если сущность не найдена
        /// </summary>
        /// <param name="type">Тип сущности</param>
        /// <param name="id">Id сущности</param>
        public EntityNotFoundException(string type, Guid id)
            : this(type, id.ToString()) { }

        /// <summary>
        /// Ошибка, возникающая если сущность не найдена
        /// </summary>
        /// <param name="type">Тип сущности</param>
        /// <param name="id">Id сущности</param>
        public EntityNotFoundException(Type type, string id)
            : this(type.ToString(), id) { }

        /// <summary>
        /// Ошибка, возникающая если сущность не найдена
        /// </summary>
        /// <param name="type">Тип сущности</param>
        /// <param name="id">Id сущности</param>
        public EntityNotFoundException(Type type, Guid id)
            : this(type.ToString(), id.ToString()) { }
    }
}
