using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BluSchool.Models
{
    public class LessonModel
    {
        public int Id { get; set; }
        public string LessonName { get; set; }
        public string ClassRoom { get; set; }
        public string Department { get; set; }
        public string Degree { get; set; }
        public int Credit { get; set; }
        public int StudentCount { get; set; }

        public string TeacherId { get; set; }
    }
}
