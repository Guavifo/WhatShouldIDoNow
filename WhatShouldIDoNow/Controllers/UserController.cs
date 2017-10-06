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
        private readonly IUserSignUpService _userSignUpService;

        public UserController(
            ISecurityService securityService, 
            IUserQueries userQueries,
            IUserSignUpService userSignUpService)
        {
            _securityService = securityService;
            _userQueries = userQueries;
            _userSignUpService = userSignUpService;
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
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!_userSignUpService.IsEmailAvailable(viewModel.Email))
            {
                ModelState.AddModelError("Email", "That email is not available.");
            }

            if (!_userSignUpService.IsUsernameAvailable(viewModel.Username))
            {
                ModelState.AddModelError("Username", "That username is not available.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _userSignUpService.SignUpUser(viewModel.Email, viewModel.Username, viewModel.Password);

            return RedirectToAction("SignIn");
        }
    }
}