using FoodDash.Web.Models.Account;
using FoodDash.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodDash.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserService _userService;
        private readonly IToastNotification _notyf;

        public AccountController(ILogger<AccountController> logger, UserService userService, IToastNotification notyf)
        {
            _logger = logger;
            _userService = userService;
            _notyf = notyf;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (_userService.IsUserLoggedIn(User))
            {
                return RedirectToAction("Index", "Restaurant");
            }

            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.SignInAsync(model);
                    if (result.Succeeded)
                    {
                        _notyf.AddSuccessToastMessage("Login successful");
                        return RedirectToAction("Index", "Restaurant");
                    }
                    _notyf.AddErrorToastMessage("Login failed");
                    ModelState.AddModelError("", "Invalid login attempt");
                }

                return View(model);
            }
            catch
            {
                return View("~/Views/Account/Login.cshtml");
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.SignOutUserAsync();

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Logout failed");
                return RedirectToAction("Index", "Restaurant");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (_userService.IsUserLoggedIn(User))
            {
                return RedirectToAction("Index", "Restaurant");
            }

            return View("~/Views/Account/Register.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.RegisterAsync(model) ;
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");
                        _notyf.AddSuccessToastMessage("Your account has been created. Please log in.");
                        return RedirectToAction("Login");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Register failed");
                return View("~/Views/Account/Register.cshtml");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var model = _userService.GetProfile(currentUserId);

                return View("~/Views/Account/Profile.cshtml", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "View profile failed");
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(ProfileModel model)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _userService.UpdateProfileAsync(model, currentUserId);
                _notyf.AddSuccessToastMessage("Your profile details have been updated.");

                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Edit profile failed");
                return RedirectToAction("Profile");
            }
        }

        [Authorize]
        public IActionResult GetUserPhoto()
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var photo = _userService.GetUserPhoto(currentUserId);

                return File(photo, "image/jpg");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to fetch photo");
                return null;
            }
        }
    }
}
