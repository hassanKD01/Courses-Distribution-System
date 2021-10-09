using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_Distribution_System.ViewModels
{
    public class SectionsListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseHours { get; set; }
        public int TdHours { get; set; }
        public int TpHours { get; set; }
        public string AcademicYear { get; set; }
        public string ProfName { get; set; }
    }
}
