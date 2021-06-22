using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.Elements
{
    public class Paragraph : ContentElement
    {
        public string Text { get; set; }

        public override string GetContent()
        {
            return Text;
        }
    }
}
