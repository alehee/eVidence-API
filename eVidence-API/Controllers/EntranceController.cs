using eVidence_API.Context;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class EntranceController : ControllerBase
    {
        private readonly ILogger<EntranceController> _logger;

        public EntranceController(ILogger<EntranceController> logger)
        {
            _logger = logger;
        }
    }
}
