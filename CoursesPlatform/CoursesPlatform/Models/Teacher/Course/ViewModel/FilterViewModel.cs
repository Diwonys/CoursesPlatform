using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.ViewModel
{
    public class FilterViewModel
    {
        public SelectList Categories { get; private set; }
        public int[] SelectedCategories { get; set; }

        public int SelectedCostFrom { get; set; }
        public int SelectedCostTo { get; set; }

        public FilterViewModel(List<CourseCategory> categories, CourseFavorPropertiesViewModel properties)
        {
            Categories = new SelectList(categories, "Id", "Name", properties.CategoriesId);
            SelectedCategories = properties.CategoriesId ?? new int[0];
            SelectedCostFrom = properties.CostFrom ?? 0;
            SelectedCostTo = properties.CostTo ?? int.MaxValue;
        }
    }
}
