using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parkyou.Interfaces;
using Parkyou.Models;

namespace Parkyou.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IParkingSpotRepository _parkingSpotRepository;
        private readonly IAdministratorRepository _administratorRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IUserRepository userRepository,
            IParkingSpotRepository parkingSpotRepository,
            IAdministratorRepository administratorRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _parkingSpotRepository = parkingSpotRepository;
            _administratorRepository = administratorRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Login(AuthenticationModel authenticationModel)
        {
            if (authenticationModel.FailedLastLogin == 0)
            {
                ModelState.ClearValidationState("Username");
                ModelState.ClearValidationState("Password");
            }

            return View(authenticationModel);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(AuthenticationModel authenticationModel)
        {
            Microsoft.AspNetCore.Identity.SignInResult signInResult;
            if (_userRepository.GetByUsername(authenticationModel.Username) is var user && user != null)
            {
                if (!_userManager.Users.Contains(user))
                    await _userManager.CreateAsync(user);
                signInResult = await _signInManager.PasswordSignInAsync(user, authenticationModel.Password, authenticationModel.KeepLoggedIn, false);
            }
            else if (_administratorRepository.GetByUsername(authenticationModel.Username) is var admin && admin != null)
            {
                if (!_userManager.Users.Contains(admin))
                    await _userManager.CreateAsync(admin);
                signInResult = await _signInManager.PasswordSignInAsync(admin, authenticationModel.Password, authenticationModel.KeepLoggedIn, false);
            }
            else
            {
                ModelState.AddModelError("Username", "Incorrect username");
                authenticationModel.FailedLastLogin = 1;
                return Login(authenticationModel);
            }
            if (signInResult.Succeeded)
            {
                authenticationModel.FailedLastLogin = 0;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("Password", "Incorrect password");
            authenticationModel.FailedLastLogin = 1;
            return Login(authenticationModel);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            this.HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Register(RegisterViewModel vm)
        {
            if (vm.FailedLastTry == 0)
            {
                ModelState.ClearValidationState("Username");
                ModelState.ClearValidationState("Email");
                ModelState.ClearValidationState("Password");
                ModelState.ClearValidationState("ConfirmPassword");
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> REgister(RegisterViewModel vm)
        {
            bool failed = !ModelState.IsValid;
            
            if (_userRepository.GetByUsername(vm.Username) != null)
            {
                ModelState.AddModelError("Username", "Username already taken!");
                failed = true;
            }
            if (_userRepository.GetByEmail(vm.Email) != null)
            {
                ModelState.AddModelError("Email", "An account with this email address already exists!");
                failed = true;
            }
            if (vm.Password != vm.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password and confirmation password are different!");
                failed = true;
            }

            if (failed)
            {
                vm.FailedLastTry = 1;
                return Register(vm);
            }

            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            ApplicationUser newUser = new ApplicationUser();

            if (currentUser.Rol == "Administrator")
            { 
                newUser = new Administrator()
                {
                    UserName = vm.Username,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Rol = "Administrator"
                };
            }
            else
            {
                newUser = new User()
                {
                    UserName = vm.Username,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Rol = "User"
                };
                ((User)newUser).ParkSpot = null;
            }
            
            var result = await _userManager.CreateAsync(newUser, vm.Password);

            if (result.Succeeded)
            {
                if (newUser.Rol == "User")
                    await _signInManager.SignInAsync(newUser, false);
                vm.FailedLastTry = 0;
                return Redirect("Index");
            }
            vm.FailedLastTry = 1;
            ModelState.AddModelError("Password", "Password is too simple!");
            return Register(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}