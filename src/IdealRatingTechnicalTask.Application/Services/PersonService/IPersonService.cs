using IdealRatingTechnicalTask.Application.Common;
using IdealRatingTechnicalTask.Application.DTOs.Person;

namespace IdealRatingTechnicalTask.Application.Services.PersonService
{
    public interface IPersonService
    {
        Task<ApiResponse<IEnumerable<PersonListResponseDTO>>> GetAllPersonsAsync(PersonFilterDTO filter);
    }
}
