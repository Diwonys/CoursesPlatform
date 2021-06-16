﻿using CoursesPlatform.Models.Teacher.Course;
using System;
using System.Collections.Generic;
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

        public List<Lesson> Lessons { get; set; }

        public Course()
        {
            Lessons = new List<Lesson>();
        }

        public int CourseCategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
    }
}
