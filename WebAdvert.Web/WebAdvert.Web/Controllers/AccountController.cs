using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime.Internal.Transform;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Web.Models.Account;

namespace WebAdvert.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _userPool;
        public AccountController(SignInManager<CognitoUser> signInManager, UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userPool = pool;
        }
        public IActionResult Signup()
        {
            var model = new SignUpModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userPool.GetUser(model.Email);
                if (user.Status != null)
                {
                    ModelState.AddModelError("UserExists", "User with this email already exists");
                    return View(model);
                }
                
                user.Attributes.Add(Convert.ToString(CognitoAttribute.Name), model.Email);
                var createdUser = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
                if (createdUser.Succeeded)
                {
                    RedirectToAction("Confirm");
                }
            }
            return View(model);
        }

        public IActionResult Confirm()
        {
            var model = new ConfirmModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(ConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    ModelState.AddModelError("NotFound", "A user with given Email was not found");
                    return View(model);
                }


                var result = await (_userManager as CognitoUserManager<CognitoUser>).ConfirmSignUpAsync(user,model.Code,true).ConfigureAwait(false);
                //var result = await _userManager.ConfirmEmailAsync(user, model.Code);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}