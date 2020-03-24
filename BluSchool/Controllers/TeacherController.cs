using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BluSchool.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }
        public IActionResult CreateLesson()
        {
            return View();
        }
    }
}