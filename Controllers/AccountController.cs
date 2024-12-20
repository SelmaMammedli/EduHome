﻿using EduHome.Enums;
using EduHome.Models;
using EduHome.Services.Interfaces;
using EduHome.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace EduHome.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, IEmailService emailService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new();
            user.FullName = registerVM.FullName;
            user.Email = registerVM.Email;
            user.UserName = registerVM.UserName;
            user.IsActive = true;
            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);

            }
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            //email confirmation
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(VerifyEmail), "Account", new { token, email = user.Email },
                Request.Scheme, Request.Host.ToString());
            string body = String.Empty;
            using (StreamReader streamReader = new StreamReader("wwwroot/emailTemplate/verifyEmailTemplate.html"))
            {
                body = streamReader.ReadToEnd();
            }
            body = body.Replace("{{name}}", user.FullName);
            body = body.Replace("{{link}}", link);
            _emailService.SendEmail(user.Email, link, "Verify EduHome email", "Verify email", body);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email or UserName or Password invalid!");
                    return View(loginVM);
                }
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Blocked!");
                return View(loginVM);
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Emaili verify et...!");
                return View(loginVM);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or UserName or Password invalid!");
                return View(loginVM);
            }
            if (!user.IsActive)
            {
                ModelState.AddModelError("", "Your account is blocked!");
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home");
        } 
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                ModelState.AddModelError("Email", "Email movcud deyil");
                return View();
            }

            //send email part
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email },
                Request.Scheme, Request.Host.ToString());
            string body = $"<a href={link}>Please click here to reset password</a>";
            _emailService.SendEmail(user.Email, link, "Forget Password for EduHome", "Reset Password", body);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            bool result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (!result)
            {
                return Content("token expired");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, ResetPasswordVM resetPasswordVM)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (!ModelState.IsValid)
                return View();
            await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.Password);
            await _userManager.UpdateSecurityStampAsync(user);
            return Json(new { message = "password ok" });
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                await _roleManager.CreateAsync(new() { Name = role.ToString() });
            }
        

            return Content("Roles added");
        }
    }
}
