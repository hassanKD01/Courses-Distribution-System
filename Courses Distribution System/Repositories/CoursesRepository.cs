using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_Distribution_System.Data;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Courses_Distribution_System.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CoursesRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<T>> GetCoursesAsync<T>(string department, int? semester, bool archived,
            string sortOrder, string searchString, string academicYear) where T : BaseCoursesListVM
        {
            var courses = _context.Courses.Where(c => c.DepartmentName.Equals(department)
            && c.Archived == archived);
            if (semester != null) courses = courses.Where(c => c.Semester == semester);

            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Code.Equals(searchString));
            }
            var coursesView = (IQueryable<T>)null;
            if (typeof(T) == typeof(CourseSectionsViewModel))
            {
                coursesView = (IQueryable<T>)courses.Select(c => new CourseSectionsViewModel
                {
                    Code = c.Code,
                    Credits = c.Credits,
                    Id = c.Id,
                    CourseHours = c.CourseHours,
                    Language = c.Language,
                    Sections = c.Sections
                    .Where(s => s.AcademicYear.Equals(academicYear)).Select(s => new SectionsListViewModel
                    {
                        AcademicYear = s.AcademicYear,
                        CourseHours = s.CourseHours,
                        Id = s.Id,
                        Name = s.Name,
                        ProfName = s.Professor.FirstName + " " + s.Professor.LastName,
                        TdHours = s.TdHours,
                        TpHours = s.TpHours
                    }),
                    TdHours = c.TdHours,
                    TpHours = c.TpHours
                });
            }

            else
                coursesView = _mapper.ProjectTo<T>(courses);

            coursesView = sortOrder switch
            {
                "code_desc" => coursesView.OrderByDescending(c => c.Code),
                _ => coursesView.OrderBy(c => c.Code)
            };
            return await coursesView.AsNoTracking().ToListAsync();
        }

        public async Task ArchiveCourse(Boolean archive, int id)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE Courses set Archived = {0} where Id = {1}", archive, id);
        }

        public async Task<T> GetCourseAsync<T>(int? id, string department) where T : BaseCourseVM
        {
            return await _mapper.ProjectTo<T>(_context.Courses).FirstOrDefaultAsync(c => c.Id == id && c.DepartmentName == department);
        }

        public async Task<int> DeleteCourse(int? id)
        {
            var course = await _context.Courses.FindAsync(id);
            int semester = -1;
            if (course != null)
            {
                semester = course.Semester;
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return semester;
        }

        public async Task<bool> AddCourse(Course course)
        {
            if (await _context.Courses.FirstOrDefaultAsync(c => c.Code == course.Code && c.Language == course.Language) != null)
            {
                return false;
            }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            _context.Attach(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
    }
}
