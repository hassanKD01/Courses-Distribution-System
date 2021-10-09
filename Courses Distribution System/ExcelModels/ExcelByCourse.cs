using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_Distribution_System.ViewModels;

namespace Courses_Distribution_System.ExcelModels
{
    public class ExcelByCourse : BaseCoursesListVM
    {
        public int Semester { get; set; }
        public string Description { get; set; }
        public int TotalHours { get; set; }
        public IList<ExcelSectionsByCourse> Sections { get; set; }
    }
}
