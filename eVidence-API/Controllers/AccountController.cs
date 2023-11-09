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

        #endregion

        #region Temporary Card

        [HttpPost, Route("temporary/register")]
        public Response TemporaryRegister(string keycard)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    if (context.TemporaryCards.Where(a => a.Keycard == keycard).Any())
                    {
                        return new Response { Success = false, Result = "Temporary card already registered" };
                    }

                    context.TemporaryCards.Add(new TemporaryCard
                    {
                        Keycard = keycard
                    });
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AccountController, TemporaryRegister", null);
                    return new Response { Success = false };
                }
            }

            return new Response();
        }

        #endregion
    }
}