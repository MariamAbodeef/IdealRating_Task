using IdealRatingTechnicalTask.Domain.Models;

namespace IdealRatingTechnicalTask.Domain.Abstraction
{
    public interface ICompositePersonService
    {
        public Task<IEnumerable<DetailedPerson>> GetAllPersonsAsync(CompositePersonFilter filterDTO);
    }
}
