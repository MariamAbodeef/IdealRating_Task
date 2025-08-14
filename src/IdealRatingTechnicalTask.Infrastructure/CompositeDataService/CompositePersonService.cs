using IdealRatingTechnicalTask.Domain.Abstraction;
using IdealRatingTechnicalTask.Domain.Entities;
using IdealRatingTechnicalTask.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;


namespace IdealRatingTechnicalTask.Infrastructure.CompositeDataService
{
    public class CompositePersonService : ICompositePersonService
    {
        private readonly IPersonExcelReader _personExcelReader;
        private readonly IPersonRepo _personRepo;
        private readonly string _excelFilePath;
        public CompositePersonService(IConfiguration configuration, IPersonExcelReader personExcelReader, IPersonRepo personRepo)
        {
            _excelFilePath = configuration["ExcelSettings:PersonsFilePath"];
            _personExcelReader = personExcelReader;
            _personRepo = personRepo;
        }
        public async Task<IEnumerable<DetailedPerson>> GetAllPersonsAsync(CompositePersonFilter filterDTO)
        {
            var dbPersons = GetDbPersons(GetDbPersonFilter(filterDTO));
            var excelPersonTask = GetExcelPersons(GetExcelPersonFilter(filterDTO));
            await Task.WhenAll(excelPersonTask);
            var excelPersons = await excelPersonTask;
            return await CompineAllPersons(dbPersons, excelPersons);

        }

        private Task<IEnumerable<ExcelPerson>> GetExcelPersons(Expression<Func<ExcelPerson, bool>> filter)
        {
            if (_excelFilePath == null)
                throw new ArgumentNullException(nameof(_excelFilePath));
            return _personExcelReader.GetAllPersonsAsync(_excelFilePath, filter);
        }

        private Expression<Func<ExcelPerson, bool>> GetExcelPersonFilter(CompositePersonFilter filterDTO)
        {
            return (person => $"{person.FirstName} {person.LastName}".ToLower().Contains(filterDTO.Name.ToLower()));
        }

        private IQueryable<Person> GetDbPersons(Expression<Func<Person, bool>> filter)
        {
            return _personRepo.GetAllPersonsAsync(filter);
        }

        private Expression<Func<Person, bool>> GetDbPersonFilter(CompositePersonFilter filterDTO)
        {
            return (person => person.Name.ToLower().Contains(filterDTO.Name.ToLower()));
        }

        private async Task<IEnumerable<DetailedPerson>> CompineAllPersons(IQueryable<Person> dbPersons, IEnumerable<ExcelPerson> excelPersons)
        {
            return excelPersons.Select(person => person.ToDetailedPerson()).ToList()
                .Concat(await dbPersons.Select(person => person.ToDetailedPerson()).ToListAsync());

        }
    }
}
