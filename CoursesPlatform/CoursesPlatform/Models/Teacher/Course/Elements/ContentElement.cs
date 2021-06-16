using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.Elements
{
    public class ContentElement
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public string Discriminator { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
