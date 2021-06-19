using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.Elements.ViewModels
{
    public class ContentViewModel
    {
        public int Id { get; set; }
        
        public string Topic { get; set; }

        public List<ElementViewModel> Elements { get; set; }

        public int LessonId { get; set; }

        public List<ContentElement> GetLesson()
        {
            List<ContentElement> contentElements = new List<ContentElement>();

            foreach (var element in Elements)
            {
                contentElements.Add(element.GetContentElement());
            }

            return contentElements;
        }

    }
}
