using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_Distribution_System.ExcelModels
{
    public class ExcelSectionsByProf
    {
        public string Description { get; set; }
        public int Semester { get; set; }
        public string Code { get; set; }
        public int CourseHours { get; set; }
        public int TdHours { get; set; }
        public int TpHours { get; set; }
        public char Language { get; set; }
    }
}
