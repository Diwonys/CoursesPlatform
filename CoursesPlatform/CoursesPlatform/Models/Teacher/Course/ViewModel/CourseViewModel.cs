using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.ViewModel
{
    public class CourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}
