using CsvHelper;
using IdealRatingTechnicalTask.Domain.Abstraction;
using IdealRatingTechnicalTask.Domain.Models;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;

namespace IdealRatingTechnicalTask.Infrastructure.ExcelReader
{
    public class PersonExcelReader : IPersonExcelReader
    {
        public async Task<IEnumerable<ExcelPerson>> GetAllPersonsAsync(string filePath, Expression<Func<ExcelPerson, bool>> filter)
        {
            return await Task.Run(() =>
            {
                var table = ReadCSVFile(filePath);
                var persons = MapPersons(table);

                return FilterPersons(persons, filter);

            });
        }

        private IEnumerable<ExcelPerson> FilterPersons(IEnumerable<ExcelPerson> persons, Expression<Func<ExcelPerson, bool>> filter)
        {
            return filter != null ? persons.AsQueryable().Where(filter).ToList() : persons;
        }

        private IEnumerable<ExcelPerson> MapPersons(DataTable table)
        {
            var persons = new List<ExcelPerson>();
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    var person = new ExcelPerson
                    {
                        FirstName = row["First Name"]?.ToString() ?? string.Empty,
                        LastName = row["Last Name"]?.ToString() ?? string.Empty,
                        TelephoneCode = row["Country code"]?.ToString() ?? string.Empty,
                        TelephoneNumber = row["Number"]?.ToString() ?? string.Empty,
                        FullAddress = row["Full Address"]?.ToString() ?? string.Empty
                    };

                    persons.Add(person);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return persons;
        }

        private DataTable ReadCSVFile(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var dataTable = new DataTable();

            csv.Read();
            csv.ReadHeader();
            foreach (var header in csv.HeaderRecord)
            {
                dataTable.Columns.Add(header);
            }

            while (csv.Read())
            {
                var row = dataTable.NewRow();
                for (int i = 0; i < csv.HeaderRecord.Length; i++)
                {
                    row[i] = csv.GetField(i);
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
