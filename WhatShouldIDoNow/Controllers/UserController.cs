using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using System.Threading.Tasks;
using WhatShouldIDoNow.DataAccess;
using WhatShouldIDoNow.Models;
using WhatShouldIDoNow.Services;

namespace WhatShouldIDoNow.Controllers
{
    public class UserController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUserQueries _userQueries;

        public UserController(ISecurityService securityService, IUserQueries userQueries)
        {
            _securityService = securityService;
            _userQueries = userQueries;
        }

        public IActionResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_securityService.VerifyUserPassword(model.UserName, model.Password) != true)
            {
                ModelState.AddModelError("UserName", "These credentials aren't valid.");
                return View(model);
            }

            var user = _userQueries.GetUserByUserName(model.UserName);
            await _securityService.SignIn(user.Id);

            return Redirect("/");
        }

        public async Task<ActionResult> SignOut()
        {
            await _securityService.SignOut();
            return Redirect("/");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateRecaptcha]
        public ActionResult SignUp(SignUpViewModel viewModel)
        {
            return View();
        }

    }
}