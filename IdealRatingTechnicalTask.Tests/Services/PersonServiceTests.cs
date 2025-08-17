using IdealRatingTechnicalTask.Application.DTOs.Person;
using IdealRatingTechnicalTask.Application.Services.PersonService;
using IdealRatingTechnicalTask.Domain.Abstraction;
using IdealRatingTechnicalTask.Domain.Models;
using Moq;
using Xunit;

namespace IdealRatingTechnicalTask.Tests.Services
{
    public class PersonServiceTests
    {
        private readonly Mock<ICompositePersonService> _mockCompositeService;
        private readonly PersonService _personService;

        public PersonServiceTests()
        {
            _mockCompositeService = new Mock<ICompositePersonService>();
            _personService = new PersonService(_mockCompositeService.Object);
        }

        [Fact]
        public async Task GetPersonsAsync_ReturnsSuccessResponse_WithValidFilter()
        {
            // Arrange
            var filterDto = new PersonFilterDTO
            {
                Name = "John"
            };

            var expectedPersons = new List<DetailedPerson>
            {
                new DetailedPerson
                {
                    FirstName = "John",
                    LastName = "Doe",
                    TelephoneCode = "+1",
                    TelephoneNumber = "1234567890",
                    Address = "123 Main St",
                    Country = "USA"
                },
                new DetailedPerson
                {
                    FirstName = "John",
                    LastName = "Smith",
                    TelephoneCode = "+44",
                    TelephoneNumber = "0987654321",
                    Address = "456 Elm St",
                    Country = "UK"
                }
            };
            _mockCompositeService.Setup(s => s.GetAllPersonsAsync(It.IsAny<CompositePersonFilter>()))
                               .ReturnsAsync(expectedPersons);


            // Act
            var result = await _personService.GetAllPersonsAsync(filterDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.All(result.Result, p => Assert.Contains("John", p.FirstName));
        }

        [Fact]
        public async Task GetPersonsAsync_ReturnsEmptyList_WhenNoPersonsMatch()
        {
            // Arrange
            var filterDto = new PersonFilterDTO
            {
                Name = "NonExistent"
            };

            _mockCompositeService.Setup(s => s.GetAllPersonsAsync(It.IsAny<CompositePersonFilter>()))
                               .ReturnsAsync(new List<DetailedPerson>());

            // Act
            var result = await _personService.GetAllPersonsAsync(filterDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Empty(result.Result);
        }
    }
}
