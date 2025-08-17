using IdealRatingTechnicalTask.API.Controllers;
using IdealRatingTechnicalTask.Application.Common;
using IdealRatingTechnicalTask.Application.DTOs.Person;
using IdealRatingTechnicalTask.Application.Services.PersonService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IdealRatingTechnicalTask.Tests.Controllers
{
    public class PersonControllerTests
    {
        private readonly Mock<IPersonService> _mockPersonService;
        private readonly PersonController _controller;

        public PersonControllerTests()
        {
            _mockPersonService = new Mock<IPersonService>();
            _controller = new PersonController(_mockPersonService.Object);
        }

        [Fact]
        public async Task GetPersons_ReturnsOkResult_WithPersonList()
        {
            // Arrange
            var filter = new PersonFilterDTO { Name = "John" };
            var expectedResponse = new ApiResponse<IEnumerable<PersonListResponseDTO>>
                (new List<PersonListResponseDTO>
                {
                    new PersonListResponseDTO {
                        FirstName = "John",
                        LastName = "Doe",
                        TelephoneCode = "+1",
                        TelephoneNumber = "1234567890",
                        Address = "123 Main St",
                        Country = "USA"
                    }
            });

            _mockPersonService.Setup(s => s.GetAllPersonsAsync(It.IsAny<PersonFilterDTO>()))
                             .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetAllPersonsAsync(filter);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<IEnumerable<PersonListResponseDTO>>>(okResult.Value);
            Assert.True(response.IsSuccess);
            Assert.Single(response.Result);
            Assert.Equal("John Doe", response.Result.First().FirstName +" "+ response.Result.First().LastName);
        }

        [Fact]
        public async Task GetPersons_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var filter = new PersonFilterDTO();
            _mockPersonService.Setup(s => s.GetAllPersonsAsync(It.IsAny<PersonFilterDTO>()))
                             .ThrowsAsync(new Exception("Database error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _controller.GetAllPersonsAsync(filter));

            Assert.Equal("Database error", exception.Message);
        }

        
    }

}
