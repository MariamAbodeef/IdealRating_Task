using IdealRatingTechnicalTask.Application.DTOs.Person;
using IdealRatingTechnicalTask.Domain.Models;

namespace IdealRatingTechnicalTask.Application.Mapping.PersonMapping
{
    public static class PersonFilterMapping
    {
        public static CompositePersonFilter ToCompositeFilter(this PersonFilterDTO dto)
        {
            return new CompositePersonFilter
            {
                Name = dto.Name,
            };
        }
    }
}
