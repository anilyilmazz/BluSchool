using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BluSchool.Models;
using BluSchool.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BluSchool.Areas.Identity.Pages.Account;
using Microsoft.Extensions.Logging;

namespace BluSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _ctx;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;


        public HomeController(ApplicationDbContext _ctx,
            RoleManager<IdentityRole> _roleManager,
            UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            this._ctx = _ctx;
            this._roleManager = _roleManager;
            this._userManager = _userManager;
            _signInManager = signInManager;
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }

       
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToAction("Index");

        }
    }
}
