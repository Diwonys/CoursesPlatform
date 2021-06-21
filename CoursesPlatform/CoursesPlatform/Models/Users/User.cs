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
        public User()
        {
            PublishedCourses = new();
            StudiedCourses = new();
        }

        public List<Course> PublishedCourses { get; set; }
        public List<Course> StudiedCourses { get; set; }
    }
}
