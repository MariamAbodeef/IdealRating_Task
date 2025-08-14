using IdealRatingTechnicalTask.Domain.Entities;
using System.Linq.Expressions;


namespace IdealRatingTechnicalTask.Domain.Abstraction
{
    public interface IPersonRepo
    {
        public IQueryable<Person> GetAllPersonsAsync(Expression<Func<Person, bool>> filter);
    }
}
