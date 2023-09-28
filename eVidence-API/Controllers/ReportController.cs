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

                    return new Response { Result = new ReportEntrance { AccountEntrances = accountHistoryOnBoard, TemporaryEntrances = temporaryHistoryOnBoard } };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ReportController, GetOnBoard", null);
                    return new Response { Success = false };
                }
            }
        }

        [HttpGet, Route("entrance")]
        public Response GetEntrance(DateTime start, DateTime stop)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var accountHistoryOnBoard = context.EntranceHistory.Where(a => a.Enter >= start).Where(a => a.Enter <= stop).Include("Account").ToList();
                    var temporaryHistoryOnBoard = context.TemporaryEntranceHistory.Where(a => a.Enter >= start).Where(a => a.Enter <= stop).ToList();

                    return new Response { Result = new ReportEntrance { AccountEntrances = accountHistoryOnBoard, TemporaryEntrances = temporaryHistoryOnBoard } };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ReportController, GetEntrance", null);
                    return new Response { Success = false };
                }
            }
        }

        [HttpGet, Route("process")]
        public Response GetProcess(DateTime start, DateTime stop)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var processesHistory = context.ProcessesHistory.Where(a => a.Start >= start).Where(a => a.Start <= stop).Include("Account").Include("TemporaryEntrance").ToList();

                    return new Response { Result = processesHistory };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ReportController, GetProcess", null);
                    return new Response { Success = false };
                }
            }
        }
    }
}