using Microsoft.AspNetCore.Mvc;
using eVidence_API.Models.Context;
using eVidence_API.Models.Helpers;
using eVidence_API.Context;
using eVidence_API.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
        public Response Check(string keycard)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var account = context.Accounts.Where(a => a.DeletedAt == null).Where(a => a.Keycard == keycard);
                    if (account.Any())
                        return new Response { Result = new CardAssignation { Type = CardType.Account, Instance = account.Single() } };

                    var temporaryCard = context.TemporaryCards.Where(a => a.Keycard == keycard);
                    if (temporaryCard.Any())
                    {
                        return new Response { Result = new CardAssignation { Type = CardType.Temporary, Instance = temporaryCard.Single() } };
                    }
                }

                return new Response { Result = new CardAssignation { Type = CardType.Unsigned } };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AccountController, Check", null);
                return new Response { Success = false };
            }
        }

        #region Default card

        [HttpPost, Route("register")]
        public Response Register(string keycard, string name, string surname, int departmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.Accounts.Add(new Account { 
                        Keycard = keycard, 
                        Name = name, 
                        Surname = surname, 
                        Department = context.Departments.Where(a => a.Id == departmentId).Single()
                    });
                    context.SaveChanges();
                } catch (Exception ex)
                {
                    _logger.LogError(ex, "AccountController, Register", null);
                    return new Response { Success = false };
                }
            }

            return new Response();
        }

        [HttpGet, Route("")]
        public Response GetAllAccounts()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return new Response { Result = context.Accounts.Include("Department").ToArray() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AccountController, GetAllAccounts", null);
                return new Response { Success = false };
            }
        }

        [HttpGet, Route("{id}")]
        public Response GetAccount(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return new Response { Result = context.Accounts.Where(a => a.Id == id).Single() };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AccountController, GetAccount", id);
                return new Response { Success = false };
            }
        }

        [HttpPut, Route("{id}")]
        public Response Edit(int id, int departmentId, string name, string surname)
        {
            try
            {
                using(var context = new ApplicationDbContext())
                {
                    var account = context.Accounts.Where(a => a.Id == id);
                    if (!account.Any())
                        return new Response { Success = false, Result = "No account found for the id" };

                    var department = context.Departments.Where(a => a.Id == departmentId);
                    if (!department.Any())
                        return new Response { Success = false, Result = "No department found for the id" };

                    var singleAccount = account.Single();
                    singleAccount.Name = name;
                    singleAccount.Surname = surname;
                    singleAccount.Department = department.Single();
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AccountController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                return new Response { Success = false };
            }

            return new ();
        }

        [HttpPatch, Route("{id}/resetkeycard")]
        public Response ResetKeycard(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var account = context.Accounts.Where(a => a.Id == id);
                    if (!account.Any())
                        return new Response { Success = false, Result = "No account found for the id" };

                    account.Single().Keycard = null;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AccountController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                return new Response { Success = false };
            }
            return new();
        }

        [HttpDelete, Route("{id}")]
        public Response Delete(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var account = context.Accounts.Where(a => a.Id == id);
                    if (!account.Any())
                        return new Response { Success = false, Result = "No account found for the id" };

                    context.Accounts.Remove(account.Single());
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AccountController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                return new Response { Success = false };
            }
            return new();
        }

        #endregion

        #region Temporary Card

        [HttpGet, Route("temporary")]
        public Response TemporaryGetAll()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return new Response { Result = context.TemporaryCards.ToArray() };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AccountController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                return new Response { Success = false };
            }
        }

        [HttpGet, Route("temporary/used")]
        public Response TemporaryGetUsed()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var temporaryEntranceActive = context.TemporaryEntranceHistory.Where(a => a.Exit == null);

                    return new Response { Result = temporaryEntranceActive.Select(a => a.TemporaryCard).ToArray() };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AccountController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                return new Response { Success = false };
            }
        }

        [HttpPost, Route("temporary")]
        public Response TemporaryKeycardCreate(string keycard)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    if (context.TemporaryCards.Where(a => a.Keycard == keycard).Any())
                        return new Response { Success = false, Result = "Temporary card is already registered" };

                    if (context.Accounts.Where(a => a.Keycard == keycard).Any())
                        return new Response { Success = false, Result = "Keycard is already registered" };

                    context.TemporaryCards.Add(new TemporaryCard
                    {
                        Keycard = keycard
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AccountController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                return new Response { Success = false };
            }

            return new ();
        }

        [HttpPatch, Route("temporary/{id}/resetkeycard")]
        public Response TemporaryKeycardReset(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var temporaryCard = context.TemporaryCards.Where(a => a.Id == id);
                    if (!temporaryCard.Any())
                        return new Response { Success = false, Result = "No temporary card for the id" };

                    temporaryCard.Single().Keycard = null;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AccountController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                return new Response { Success = false };
            }

            return new ();
        }

        [HttpDelete, Route("temporary/{id}")]
        public Response TemporaryKeycardDelete(int id)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var temporaryCard = context.TemporaryCards.Where(a => a.Id == id);
                    if (!temporaryCard.Any())
                        return new Response { Success = false, Result = "No temporary card for the id" };

                    context.TemporaryCards.Remove(temporaryCard.Single());
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"AccountController, {MethodBase.GetCurrentMethod()}", MethodBase.GetCurrentMethod().GetParameters());
                return new Response { Success = false };
            }

            return new();
        }

        #endregion
    }
}