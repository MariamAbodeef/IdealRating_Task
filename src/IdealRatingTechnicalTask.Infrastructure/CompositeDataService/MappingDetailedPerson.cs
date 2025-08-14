using IdealRatingTechnicalTask.Domain.Entities;
using IdealRatingTechnicalTask.Domain.Models;


namespace IdealRatingTechnicalTask.Infrastructure.CompositeDataService
{
    public static class MappingDetailedPerson
    {
        public static DetailedPerson ToDetailedPerson(this Person person)
        {
            var splitedName = person.Name.Split(' ');
            var splitedTelephoneNumber = person.TelephoneNumber.Split('-');
            return new DetailedPerson
            {
                FirstName = splitedName.Length > 0 ? splitedName[0] : person.Name,
                LastName = splitedName.Length > 1 ? splitedName[1] : string.Empty,
                Address = person.Address,
                Country = person.Country,
                TelephoneCode = splitedTelephoneNumber.Length > 0 ? splitedTelephoneNumber[0] : string.Empty,
                TelephoneNumber = splitedTelephoneNumber.Length > 1 ? splitedTelephoneNumber[1] : string.Empty,
            };
        }

        public static DetailedPerson ToDetailedPerson(this ExcelPerson person)
        {
            var splitedAddress = person.FullAddress.Split(',');
            var splitedTelephoneNumber = person.TelephoneNumber.Split('-');
            return new DetailedPerson
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Address = string.Join("", splitedAddress.SkipLast(1)),
                Country = splitedAddress.Last(),
                TelephoneCode = person.TelephoneCode,
                TelephoneNumber = person.TelephoneNumber,
            };
        }
    }
}
