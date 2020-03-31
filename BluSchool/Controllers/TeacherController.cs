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
            return RedirectToAction("Index"); //ders ekledikten sonra index'e yönlendir
        }
      
        public IActionResult DeleteLesson(int id)
        {
            var delete_lesson = new LessonModel();
            delete_lesson.Id = id;
            _ctx.Lessons.Attach(delete_lesson);
            _ctx.Lessons.Remove(delete_lesson);
            _ctx.SaveChanges();
            return RedirectToAction("Index"); //ders silindikten sonra index'e yönlendir.
        }
        public IActionResult UpdateLesson(int id)
        {
            //ID ALIYORUM
            var updatedLesson = _ctx.Lessons.Where(x => x.Id == id).FirstOrDefault();
            //veritabanindan buldum simdi onu html e gonderecegim
            return View(updatedLesson);
        }

    
        [HttpPost]
        public IActionResult UpdateLesson(int id, string lessonName_frm_input, string ClassRoom, string Department, string Degree, int Credit, int StudentCount)
        {

            var update_lesson = new LessonModel();
            update_lesson = _ctx.Lessons.Where(x => x.Id == id).FirstOrDefault();
            update_lesson.LessonName = lessonName_frm_input;
            update_lesson.ClassRoom = ClassRoom;
            update_lesson.Department = Department;
            update_lesson.Degree = Degree;
            update_lesson.Credit = Credit;
            update_lesson.StudentCount = StudentCount;
            _ctx.SaveChanges();
            _ctx.Lessons.Update(update_lesson);
            return RedirectToAction("Index");
            

        }

           
  
 
        }
    }
