using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesPlatform.Models.Teacher.Course.ViewModel
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; }
        public SortState CostSort { get; private set; }
        public SortState CurrentState { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            CostSort = sortOrder == SortState.CostAsc ? SortState.CostDesc : SortState.CostAsc;
            CurrentState = sortOrder;
        }
    }
}
