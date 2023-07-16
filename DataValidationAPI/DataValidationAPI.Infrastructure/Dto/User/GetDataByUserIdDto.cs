using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidationAPI.Infrastructure.Dto.User
{
    /// <summary>
    /// Данные для получения данных
    /// </summary>
    /// <param name="Start">Начало отчета</param>
    /// <param name="Length">Длина среза</param>
    /// <param name="DateStart">Дата начала отчета</param>
    /// <param name="DateEnd">Дата окончания отчета</param>
    /// <param name="IsUnverifiedData">Взять не проверенные</param>
    /// <param name="IsValidatedData">Взять прошедшие проверку</param>
    /// <param name="IsNoValidatedData">Взять не прошедшие проверку</param>
    /// <param name="IsCheckData">Взять находящиеся на проверке</param>
    public record class GetDataByUserIdDto(
        int? Start,
        int? Length,
        DateTime? DateStart,
        DateTime? DateEnd,
        bool? IsUnverifiedData = true,
        bool? IsValidatedData = true,
        bool? IsNoValidatedData = true,
        bool? IsCheckData = true);
}
