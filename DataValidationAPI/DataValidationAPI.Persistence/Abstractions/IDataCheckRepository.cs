using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Persistence.Abstractions
{
    /// <summary>
    /// Интерфейс репозитория проверки данных
    /// </summary>
    public interface IDataCheckRepository
    {
        /// <summary>
        /// Получить по Id
        /// </summary>
        /// <param name="dataId">Id данных</param>
        /// <returns></returns>
        public Task<DataCheck?> GetById(Guid dataId);

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="entity">Добавляемая проверка данных</param>
        public Task Insert(DataCheck entity);

        /// <summary>
        /// Удалить по Id
        /// </summary>
        /// <param name="dataId">Id удаляемой проверки данных<</param>
        public Task Delete(Guid dataId);

        /// <summary>
        /// Изменить
        /// </summary>
        /// <param name="entity">Изменяемая проверка данных</param>
        public Task Update(DataCheck entity);

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public Task Save();
    }
}
