using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course
{
    public class CourseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}
