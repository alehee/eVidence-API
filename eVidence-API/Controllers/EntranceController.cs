using eVidence_API.Context;
using eVidence_API.Enums;
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

        [HttpGet, Route("check")]
        public Response Check(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // TODO
                }

                return new Response { Result = new CardAssignation { Type = CardType.Unsigned } };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EntranceController, Check", null);
                return new Response { Success = false };
            }
        }

        [HttpGet, Route("temporary/check")]
        public Response TemporaryCheck(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // TODO
                }

                return new Response { Result = new CardAssignation { Type = CardType.Unsigned } };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EntranceController, TemporaryCheck", null);
                return new Response { Success = false };
            }
        }
    }
}
