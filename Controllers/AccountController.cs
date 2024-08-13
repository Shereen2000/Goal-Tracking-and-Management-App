using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Goal_Management_Web_App.Models;
using Goal_Management_Web_App.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Goal_Management_Web_App.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager,
        SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if(ModelState.IsValid)
            {
                User user =
                await _userManager.FindByNameAsync(loginModel.Email);
                if (user != null)
                {
                    var results = await _signInManager.PasswordSignInAsync(user,loginModel.Password, isPersistent:loginModel.RememberMe, false);
                    if(results.Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Home/Index");
                    }
                }
            }
            ModelState.AddModelError("","Invalid username or password");
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

         public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                };

                IdentityResult result
                    = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                   
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }

}