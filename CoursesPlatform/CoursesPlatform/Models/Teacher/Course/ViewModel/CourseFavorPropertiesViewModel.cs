using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.ViewModel
{
    public class CourseFavorPropertiesViewModel
    {
        public int Page { get; set; } = 1;
        public SortState sortOrder { get; set; } = SortState.NameAsc;

        public int? CostFrom { get; set; }
        public int? CostTo { get; set; }

        public int[] CategoriesId { get; set; }
    }
}
