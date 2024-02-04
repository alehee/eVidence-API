using eVidence_API.Context;
using eVidence_API.Enums;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        #region Account
        [HttpGet, Route("check")]
        public Response GetCheck(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var history = context.ProcessesHistory.Where(a => a.Account.Id == id).Include(nameof(Process));
                    if (!history.Any())
                        return new Response { Result = null };

                    return new Response { Result = history.OrderBy(a => a.Id).Reverse().First() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, GetCheck", null);
                return new Response { Success = false };
            }
        }

        
        [HttpPost, Route("start/{id}")]
        public Response PostStart(int id, int departmentId, int accountId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var account = context.Accounts.Where(a => a.Id == accountId);
                    if (!account.Any())
                        return new Response { Success = false, Result = "Account with this id not exists" };

                    var department = context.Departments.Where(a => a.Id == departmentId);
                    if (!department.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    var lastProcess = context.ProcessesHistory.Where(a => a.Account == account.Single()).Where(a => a.Stop == null);
                    if (lastProcess.Any())
                        lastProcess.First().Stop = DateTime.Now;

                    var process = context.Processes.Where(a => a.Id == id);
                    if (!process.Any())
                        return new Response { Success = false, Result = "Process with this id not exists" };


                    context.ProcessesHistory.Add(new ProcessHistory { Account = account.Single(), Department = department.Single(), Process = process.Single() });
                    context.SaveChanges();

                    return new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, PostStart", null);
                return new Response { Success = false };
            }
        }

        [HttpPut, Route("stop")]
        public Response PutStop(int accountId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    
                    var account = context.Accounts.Where(a => a.Id == accountId);
                    if (!account.Any())
                        return new Response { Success = false, Result = "Account with this id not exists" };

                    var history = context.ProcessesHistory.Where(a => a.Account == account.Single()).Where(a => a.Stop == null);
                    if (!history.Any())
                        return new Response { Success = false, Result = "This account has not any ongoing processes" };

                    history.First().Stop = DateTime.Now;
                    context.SaveChanges();

                    return new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, PutStop", null);
                return new Response { Success = false };
            }
        }
        #endregion

        #region Temporary Card
        [HttpGet, Route("temporary/check")]
        public Response GetTemporaryCheck(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var history = context.ProcessesHistory.Where(a => a.TemporaryEntrance.Id == id).Include(nameof(Process));
                    if (!history.Any())
                        return new Response { Result = null };

                    return new Response { Result = history.OrderBy(a => a.Id).Reverse().First() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, GetTemporaryCheck", null);
                return new Response { Success = false };
            }
        }


        [HttpPost, Route("temporary/start/{id}")]
        public Response PostTemporaryStart(int id, int departmentId, int temporaryEntranceHistoryId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var temporaryEntrance = context.TemporaryEntranceHistory.Where(a => a.Id == temporaryEntranceHistoryId);
                    if (!temporaryEntrance.Any())
                        return new Response { Success = false, Result = "TemporaryEntrance with this id not exists" };

                    var department = context.Departments.Where(a => a.Id == departmentId);
                    if (!department.Any())
                        return new Response { Success = false, Result = "Department with this id not exists" };

                    var lastProcess = context.ProcessesHistory.Where(a => a.TemporaryEntrance == temporaryEntrance.Single()).Where(a => a.Stop == null);
                    if (lastProcess.Any())
                        lastProcess.First().Stop = DateTime.Now;

                        var process = context.Processes.Where(a => a.Id == id);
                    if (!process.Any())
                        return new Response { Success = false, Result = "Process with this id not exists" };

                    context.ProcessesHistory.Add(new ProcessHistory { TemporaryEntrance = temporaryEntrance.Single(), Department = department.Single(), Process = process.Single() });
                    context.SaveChanges();

                    return new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, PostTemporaryStart", null);
                return new Response { Success = false };
            }
        }

        [HttpPut, Route("temporary/stop")]
        public Response PutTemporaryStop(int temporaryEntranceHistoryId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {

                    var temporaryEntrance = context.TemporaryEntranceHistory.Where(a => a.Id == temporaryEntranceHistoryId);
                    if (!temporaryEntrance.Any())
                        return new Response { Success = false, Result = "TemporaryEntrance with this id not exists" };

                    var history = context.ProcessesHistory.Where(a => a.TemporaryEntrance == temporaryEntrance.Single()).Where(a => a.Stop == null);
                    if (!history.Any())
                        return new Response { Success = false, Result = "This temporaryEntrance has not any ongoing processes" };

                    history.First().Stop = DateTime.Now;
                    context.SaveChanges();

                    return new();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CheckpointController, PutTemporaryStop", null);
                return new Response { Success = false };
            }
        }
        #endregion
    }
}
