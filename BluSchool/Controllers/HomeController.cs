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

        [Authorize(Roles = "Admin")]
        public IActionResult UserList()
        {
            var userList = _ctx.Users.ToList();
            return View(userList);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeTeacher(string userId)
        {
            var user = _ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
            var result1 = await _userManager.AddToRoleAsync(user, "Teacher");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeStudent(string userId)
        {
            var user = _ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
            var result1 = await _userManager.AddToRoleAsync(user, "Student");
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> initRole()
        {
            bool x = await _roleManager.RoleExistsAsync("Admin");
            if (!x)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);

                var teacherRole = new IdentityRole();
                role.Name = "Teacher";
                await _roleManager.CreateAsync(teacherRole);

                var studentRole = new IdentityRole();
                role.Name = "Student";
                await _roleManager.CreateAsync(studentRole);
                //Here we create a Admin super user who will maintain the website                   

                var user = new IdentityUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";

                string userPWD = "asdasdasD1.";

                IdentityResult chkUser = await _userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, "Admin");
                }
               
            }
            return Json(1);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToAction("Index");

        }
    }
}
