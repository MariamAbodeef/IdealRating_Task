using IdealRatingTechnicalTask.Application.DTOs.Person;
using IdealRatingTechnicalTask.Application.Services.PersonService;
using Microsoft.AspNetCore.Mvc;

namespace IdealRatingTechnicalTask.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService personService;

        public PersonController(IPersonService _personService)
        {
            personService = _personService;
        }

        [HttpPost("GetAllPersons")]
        public async Task<IActionResult> GetAllPersonsAsync([FromBody] PersonFilterDTO personFilterDTO)
        {
            {
                return Ok(await personService.GetAllPersonsAsync(personFilterDTO));
            }
        }
    }
}
