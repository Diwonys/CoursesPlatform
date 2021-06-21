using CoursesPlatform.Models.Teacher.Course.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public List<ContentElement> Content { get; set; }

        public Lesson() => Content = new List<ContentElement>();

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
