using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BluSchool.Data;
using BluSchool.Models;

namespace BluSchool.Controllers
{
    public class TeacherController : Controller
    {
        private ApplicationDbContext _ctx;

        public TeacherController(ApplicationDbContext _ctx)
        {
            this._ctx = _ctx;         
        }
        public IActionResult Index()
        {
            var lessonList = _ctx.Lessons.ToList();
            ViewData["lessonList"] = lessonList;
            return View(lessonList);
        }
        public IActionResult CreateLesson()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateLesson(string lessonName_frm_input, string ClassRoom, string Department, string Degree, int Credit, int StudentCount)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newLesson = new LessonModel();
            newLesson.ClassRoom = ClassRoom;
            newLesson.Credit = Credit;
            newLesson.Degree = Degree;
            newLesson.Department = Department;
            newLesson.LessonName = lessonName_frm_input;
            newLesson.StudentCount = StudentCount;
            newLesson.TeacherId = userId;
            _ctx.Lessons.Add(newLesson);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}