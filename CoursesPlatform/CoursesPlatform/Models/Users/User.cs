using CoursesPlatform.Models.Teacher.Course;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Users
{
    public class User : IdentityUser
    {
        public List<Course> Courses { get; set; }

        public User()
        {
            Courses = new List<Course>();
        }
    }
}
