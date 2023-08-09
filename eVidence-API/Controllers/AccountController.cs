using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("check")]
        public CardAssignation Check(string keycard)
        {
            return new CardAssignation { Type = Enums.CardType.Unsigned };
        }

        [HttpPost, Route("register")]
        public Response Register(string keycard, string name, string surname, int? department = null)
        {
            return new Response { Result = true };
        }

        [HttpPost, Route("login")]
        public Account Login(string keycard)
        {
            return new Account { Id = 1, Name = "Krzysztof", Surname = "Cha³ka", Department = new Department { Id = 2, Entity = new Entity { Id = 1, Name = "Decathlon" } } };
        }
    }
}