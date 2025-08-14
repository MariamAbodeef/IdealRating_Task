using IdealRatingTechnicalTask.Application.Common;
using IdealRatingTechnicalTask.Application.DTOs.Person;
using IdealRatingTechnicalTask.Application.Mapping.PersonMapping;
using IdealRatingTechnicalTask.Domain.Abstraction;

namespace IdealRatingTechnicalTask.Application.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly ICompositePersonService _personService;
        public PersonService(ICompositePersonService personService)
        {
            _personService = personService;
        }
        public async Task<ApiResponse<IEnumerable<PersonListResponseDTO>>> GetAllPersonsAsync(PersonFilterDTO filter)
        {
            try
            {
                var getAllPersonsTask = _personService.GetAllPersonsAsync(filter.ToCompositeFilter());
                await Task.WhenAll(getAllPersonsTask);
                var persons = await getAllPersonsTask;
                return new ApiResponse<IEnumerable<PersonListResponseDTO>>(persons.Select(person => person.ToPersonListResponseDto()).ToList());
            }
            catch (Exception ex)
            {

                return new ApiResponse<IEnumerable<PersonListResponseDTO>>($"Error while retrieving Persons {ex.Message}");
            }
        }
    }
}
