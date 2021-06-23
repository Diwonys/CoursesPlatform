using CoursesPlatform.Models.Teacher.Course;
using CoursesPlatform.Models.Teacher.Course.Elements;
using CoursesPlatform.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course
{
    public class Course
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int Cost { get; set; }

        [NotMapped]
        private Image image;
        public Image Image 
        { 
            get 
            {
                if (image == null)
                    image = new();
                return image; 
            }
            set => image = value; 
        }

        public List<Lesson> Lessons { get; set; }


        public Course()
        {
            Lessons = new();
        }

        public string UserPublishedCoursesId { get; set; }
        public User UserPublishedCourses { get; set; }

        public string UserStudiedCoursesId { get; set; }
        public User UserStudiedCourses { get; set; }

        public int CourseCategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
    }
}
