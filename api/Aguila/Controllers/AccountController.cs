using Aguila.Models;
using Aguila.Models.AccountModel;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This class contains methods related to account to Change Password,
    /// Forgot Password and Logout.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, User")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        // POST api/Account/Logout
        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _accountService.Logout();
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST api/Account/ChangePassword
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _accountService.ChangePassword(model);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }


        // POST: api/Account/PasswordResetRequest
        [HttpPost]
        [Route("PasswordResetRequest")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _accountService.ForgotPassword(model);
            if (!result)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [Authorize(Roles = "Administrator")]
        //[AllowAnonymous]
        [Route("GetAccountSASToken")]
        public async Task<IActionResult> GetAccountSASToken()
        {
            string result = await _accountService.GetAccountSASToken();
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}