using IdealRatingTechnicalTask.Domain.Abstraction;
using IdealRatingTechnicalTask.Domain.Entities;
using IdealRatingTechnicalTask.Infrastructure.Context;
using System.Linq.Expressions;

namespace IdealRatingTechnicalTask.Infrastructure.Reporsitories
{
    public class PersonRepo : IPersonRepo
    {
        private readonly AppDbContext appDbContext;

        public PersonRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<Person> GetAllPersonsAsync(Expression<Func<Person, bool>> filter)
        {
                return appDbContext.Persons.AsQueryable().Where(filter);
        }
    }
}
