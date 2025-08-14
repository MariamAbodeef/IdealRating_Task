using IdealRatingTechnicalTask.Application.DTOs.Person;
using IdealRatingTechnicalTask.Domain.Models;

namespace IdealRatingTechnicalTask.Application.Mapping.PersonMapping
{
    public static class PersonMapping
    {
        public static PersonListResponseDTO ToPersonListResponseDto(this DetailedPerson dto)
        {
            return new PersonListResponseDTO
            {
                Address = dto.Address,
                Country = dto.Country,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                TelephoneCode = dto.TelephoneCode,
                TelephoneNumber = dto.TelephoneNumber
            };
        }
    }
}
