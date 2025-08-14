using IdealRatingTechnicalTask.Domain.Models;
using System.Linq.Expressions;

namespace IdealRatingTechnicalTask.Domain.Abstraction
{
    public interface IPersonExcelReader
    {
        public Task<IEnumerable<ExcelPerson>> GetAllPersonsAsync(string filePath, Expression<Func<ExcelPerson, bool>> filter);
    }
}
