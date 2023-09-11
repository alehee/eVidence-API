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

        #region Default cards

        [HttpGet, Route("check")]
        public Response Check(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var history = context.EntranceHistory.Where(a => a.Account.Id == id);

                    if (!history.Any())
                        return new Response { Result = null };

                    return new Response { Result = history.OrderBy(a => a.Id).Reverse().First() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EntranceController, Check", null);
                return new Response { Success = false };
            }
        }

        [HttpPost, Route("toggle")]
        public Response Toggle(int id, bool isEntering)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var account = context.Accounts.Where(a => a.Id == id).Single();

                    if (isEntering)
                    {
                        context.EntranceHistory.Add(new Entrance { Account = account, Enter = DateTime.Now });
                        context.SaveChanges();
                    }

                    else
                    {
                        var row = context.EntranceHistory.Where(a => a.Account == account).OrderBy(a => a.Id).Reverse().First();
                        row.Exit = DateTime.Now;
                        context.SaveChanges();
                    }

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EntranceController, Toggle", null);
                return new Response { Success = false };
            }
        }

        #endregion

        #region Temporary cards

        [HttpGet, Route("temporary/check")]
        public Response TemporaryCheck(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var history = context.TemporaryEntranceHistory.Where(a => a.TemporaryCard.Id == id);

                    if (!history.Any())
                        return new Response { Result = null };

                    return new Response { Result = history.OrderBy(a => a.Id).Reverse().First() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EntranceController, TemporaryCheck", null);
                return new Response { Success = false };
            }
        }

        [HttpPost, Route("temporary/enter")]
        public Response TemporaryEnter(int id, string name, string surname)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var temporaryCard = context.TemporaryCards.Where(a => a.Id == id).Single();

                    context.TemporaryEntranceHistory.Add(new TemporaryEntrance { TemporaryCard = temporaryCard, Name = name, Surname = surname, Enter = DateTime.Now });
                    context.SaveChanges();

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EntranceController, TemporaryEnter", null);
                return new Response { Success = false };
            }
        }

        [HttpPost, Route("temporary/exit")]
        public Response TemporaryExit(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var temporaryCard = context.TemporaryCards.Where(a => a.Id == id).Single();

                    var row = context.TemporaryEntranceHistory.Where(a => a.TemporaryCard == temporaryCard).OrderBy(a => a.Id).Reverse().First();
                    row.Exit = DateTime.Now;
                    context.SaveChanges();

                    return new Response();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EntranceController, TemporaryExit", null);
                return new Response { Success = false };
            }
        }

        #endregion
    }
}
