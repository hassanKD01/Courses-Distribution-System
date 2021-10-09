using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_Distribution_System.ExcelModels;
using OfficeOpenXml;

namespace Courses_Distribution_System.Services
{
    public class ExcelService
    {
        public byte[] DistributionReportByCourse(IList<ExcelByCourse> distribution)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Distribution by Courses");

            worksheet.Cells["A1"].Value = "رمز المادة";
            worksheet.Cells["B1"].Value = "الفصل";
            worksheet.Cells["C1"].Value = "المقرر";
            worksheet.Cells["D1"].Value = "نظري";
            worksheet.Cells["E1"].Value = "اعمال موجهة";
            worksheet.Cells["F1"].Value = "تطبيقية";
            worksheet.Cells["G1"].Value = "لغة";
            worksheet.Cells["H1"].Value = "رقم الشعبة على عدد الشعب";
            worksheet.Cells["I1"].Value = "اسم الاستاذ";
            worksheet.Cells["J1"].Value = "نوع العقد";
            worksheet.Cells["K1"].Value = "عدد الساعات";

            int start, current = 2;
            int sectionNb;
            foreach (var course in distribution)
            {
                if (course.Sections.Count > 0)
                {
                    start = current;
                    worksheet.Cells[string.Format("K{0}", start)].Value = course.TotalHours;
                    worksheet.Cells[string.Format("K{0}:K{1}", start, start + course.Sections.Count - 1)].Merge = true;
                    sectionNb = 1;
                    foreach (var section in course.Sections)
                    {
                        worksheet.Cells[string.Format("A{0}", current)].Value = course.Code;
                        worksheet.Cells[string.Format("B{0}", current)].Value = course.Semester;
                        worksheet.Cells[string.Format("C{0}", current)].Value = course.Description;
                        worksheet.Cells[string.Format("D{0}", current)].Value = section.CourseHours;
                        worksheet.Cells[string.Format("E{0}", current)].Value = section.TdHours;
                        worksheet.Cells[string.Format("F{0}", current)].Value = section.TpHours;
                        worksheet.Cells[string.Format("G{0}", current)].Value = course.Language == 'E' ? 'A' : course.Language;
                        worksheet.Cells[string.Format("H{0}", current)].Value = sectionNb + "/" + course.Sections.Count;
                        worksheet.Cells[string.Format("I{0}", current)].Value = section.NameProf;
                        worksheet.Cells[string.Format("J{0}", current)].Value = section.Contract;
                        current++;
                        sectionNb++;
                    }
                }
            }
            worksheet.Cells["A:AZ"].AutoFitColumns();
            return package.GetAsByteArray();
        }

        public byte[] DitributionReportByProfessor(IList<ExcelByProf> distribution)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Distribution by Professors");

            worksheet.Cells["A1"].Value = "رقم الملف";
            worksheet.Cells["B1"].Value = "اسم الاستاذ";
            worksheet.Cells["C1"].Value = "رتبة";
            worksheet.Cells["D1"].Value = "نوع العقد";
            worksheet.Cells["E1"].Value = "المادة";
            worksheet.Cells["F1"].Value = "الفصل";
            worksheet.Cells["G1"].Value = "الرمز";
            worksheet.Cells["H1"].Value = "Cours";
            worksheet.Cells["I1"].Value = "TD";
            worksheet.Cells["J1"].Value = "TP";
            worksheet.Cells["K1"].Value = "السنة";
            worksheet.Cells["L1"].Value = "اللغة";
            worksheet.Cells["M1"].Value = "رقم الشعبة على عدد الشعب";
            worksheet.Cells["N1"].Value = "الكلية و الفرع";
            worksheet.Cells["O1"].Value = "مجموع نصاب الاستاذ";

            int start, current = 2;
            int sectionNb;
            foreach (var professor in distribution)
            {
                if (professor.Sections.Count > 0)
                {
                    start = current;
                    worksheet.Cells[string.Format("A{0}", start)].Value = professor.Id;
                    worksheet.Cells[string.Format("B{0}", start)].Value = professor.Name;
                    worksheet.Cells[string.Format("C{0}", start)].Value = professor.AcademicRank;
                    worksheet.Cells[string.Format("D{0}", start)].Value = professor.Contract;
                    worksheet.Cells[string.Format("O{0}", start)].Value = professor.TotalHours;
                    worksheet.Cells[string.Format("A{0}:A{1}", start, start + professor.Sections.Count - 1)].Merge = true;
                    worksheet.Cells[string.Format("B{0}:B{1}", start, start + professor.Sections.Count - 1)].Merge = true;
                    worksheet.Cells[string.Format("C{0}:C{1}", start, start + professor.Sections.Count - 1)].Merge = true;
                    worksheet.Cells[string.Format("D{0}:D{1}", start, start + professor.Sections.Count - 1)].Merge = true;
                    worksheet.Cells[string.Format("O{0}:O{1}", start, start + professor.Sections.Count - 1)].Merge = true;
                    sectionNb = 1;

                    foreach (var section in professor.Sections)
                    {
                        worksheet.Cells[string.Format("E{0}", current)].Value = section.Description;
                        worksheet.Cells[string.Format("F{0}", current)].Value = section.Semester % 2 + 1;
                        worksheet.Cells[string.Format("G{0}", current)].Value = section.Code;
                        worksheet.Cells[string.Format("H{0}", current)].Value = section.CourseHours;
                        worksheet.Cells[string.Format("I{0}", current)].Value = section.TdHours;
                        worksheet.Cells[string.Format("J{0}", current)].Value = section.TpHours;
                        worksheet.Cells[string.Format("K{0}", current)].Value = Math.Ceiling((decimal)section.Semester / 2);
                        worksheet.Cells[string.Format("L{0}", current)].Value = section.Language;
                        worksheet.Cells[string.Format("M{0}", current)].Value = sectionNb + "/" + professor.Sections.Count;
                        worksheet.Cells[string.Format("N{0}", current)].Value = "العلوم 1";
                        current++;
                        sectionNb++;
                    }
                }
            }
            worksheet.Cells["A:AZ"].AutoFitColumns();
            return package.GetAsByteArray();
        }
    }
}
