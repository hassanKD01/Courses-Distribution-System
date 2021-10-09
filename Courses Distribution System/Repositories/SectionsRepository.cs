using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_Distribution_System.Data;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Courses_Distribution_System.Repositories
{
    public class SectionsRepository : ISectionsRepository
    {
        private readonly AppDbContext _context;

        public SectionsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Section> DeleteSection(int? id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return section;
            }
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return section;
        }

        public async Task AddSection(Section section)
        {
            _context.Sections.Add(section);
            await _context.SaveChangesAsync();
        }

        public async Task<SectionsListViewModel> GetSectionAsync(int id)
        {
            return await _context.Sections.Select(s => new SectionsListViewModel
            {
                AcademicYear = s.AcademicYear,
                CourseHours = s.CourseHours,
                Id = s.Id,
                Name = s.Name,
                ProfName = s.Professor.FirstName + " " + s.Professor.LastName,
                TdHours = s.TdHours,
                TpHours = s.TpHours
            }).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateSection(Section section)
        {
            _context.Attach(section).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(section.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
