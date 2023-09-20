using eVidence_API.Context;
using eVidence_API.Enums;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eVidence_API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class CheckpointController : ControllerBase
    {
        private readonly ILogger<CheckpointController> _logger;

        public CheckpointController(ILogger<CheckpointController> logger)
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
                    var history = context.ProcessesHistory.Where(a => a.Account.Id == id);

                    if (!history.Any())
                        return new Response { Result = null };

                    return new Response { Result = history.OrderBy(a => a.Id).Reverse().First() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, Check", null);
                return new Response { Success = false };
            }
        }

        [HttpPost, Route("/start/{id}")]
        public Response PostStart(int id, int departmentId, int accountId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var process = context.Processes.Where(a => a.Id == id);

                    if (!process.Any())
                        return new Response { Success = false, Result = "Process with this id not exists" };

                    var account = context.Accounts.Where(a => a.Id == accountId);

                    if (!account.Any())
                        return new Response { Success = false, Result = "Account with this id not exists" };

                    var department = context.Departments.Where(a => a.Id == departmentId);

                    if (!department.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    context.ProcessesHistory.Add(new ProcessHistory { Account = account.Single(), Department = department.Single(), Process = process.Single() });

                    return new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, PostStart", null);
                return new Response { Success = false };
            }
        }
    }
}
