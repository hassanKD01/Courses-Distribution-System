using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_Distribution_System.ViewModels;

namespace Courses_Distribution_System.ExcelModels
{
    public class ExcelByProf : BaseProfessorsVM
    {
        public string AcademicRank { get; set; }
        public string Contract { get; set; }
        public int TotalHours { get; set; }
        public IList<ExcelSectionsByProf> Sections { get; set; }
    }
}
