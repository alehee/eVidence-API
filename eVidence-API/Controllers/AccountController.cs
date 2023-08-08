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
        public AccountHeader Check(string keycard)
        {
            return new AccountHeader { Keycard = keycard };
        }
    }
}