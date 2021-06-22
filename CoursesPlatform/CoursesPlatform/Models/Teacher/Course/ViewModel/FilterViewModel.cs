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

        public FilterViewModel(List<Course> categories, int[] categoriesId, int? selectedCostFrom, int? selectedCostTo)
        {
            Categories = new SelectList(categories, "Id", "Name", categoriesId);
            SelectedCategories = categoriesId ?? new int[0];
            SelectedCostFrom = selectedCostFrom ?? 0;
            SelectedCostTo = selectedCostTo ?? int.MaxValue;
        }
    }
}
