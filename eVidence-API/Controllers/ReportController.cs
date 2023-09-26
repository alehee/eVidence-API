using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using eVidence_API.Context;
using System.Xml.Linq;
using eVidence_API.Enums;
using Microsoft.EntityFrameworkCore;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("onboard")]
        public Response GetOnBoard()
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var accountHistoryOnBoard = context.EntranceHistory.Where(a => a.Exit == null).Include("Account").ToList();
                    var temporaryHistoryOnBoard = context.TemporaryEntranceHistory.Where(a => a.Exit == null).ToList();

                    return new Response { Result = new ReportOnBoard { AccountEntrances = accountHistoryOnBoard, TemporaryEntrances = temporaryHistoryOnBoard } };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ReportController, GetOnBoard", null);
                    return new Response { Success = false };
                }
            }
        }
    }
}