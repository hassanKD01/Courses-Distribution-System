using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_Distribution_System.Data;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Courses_Distribution_System.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProfessorRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<T>> GetProfessorsAsync<T>(string department, bool archived,
            string sortOrder, string searchString) where T : BaseProfessorsVM
        {
            var professors = _context.Professors.Where(p => _context.ProfessorsDepartments
            .Where(pd => pd.DepartmentName == department).Select(pd => pd.ProfessorId).Contains(p.Id)
            && p.Archived == archived);

            if (!String.IsNullOrEmpty(searchString))
            {
                professors = professors.Where(p => p.FirstName == searchString);
            }

            var professorsView = _mapper.ProjectTo<T>(professors);
            professorsView = sortOrder switch
            {
                "id_desc" => professorsView.OrderByDescending(s => s.Id),
                "Name" => professorsView.OrderBy(s => s.Name),
                "name_desc" => professorsView.OrderByDescending(s => s.Name),
                _ => professorsView.OrderBy(s => s.Id),
            };
            return await professorsView.AsNoTracking().ToListAsync();
        }

        public async Task<Professor> GetProfessorAsync(int? id, string department)
        {
            return await _context.Professors
                .Include(p => p.ProfessorHours)
                .FirstOrDefaultAsync(p => p.Id == id
                && _context.ProfessorsDepartments
            .Where(pd => pd.DepartmentName == department).Select(pd => pd.ProfessorId).Contains(p.Id));
        }

        public async Task<bool> CreateProfessor(Professor professor)
        {
            _context.Professors.Add(professor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException exception && (exception.Number == 2627 || exception.Number == 2601))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateProfessor(Professor professor, ProfessorHours professorHours, bool add)
        {
            _context.Attach(professor).State = EntityState.Modified;
            if (add)
                _context.ProfessorHours.Add(professorHours);
            else
                _context.Attach(professorHours).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorExists(professor.Id))
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
        public async Task<bool> UpdateProfessor(Professor professor)
        {
            _context.Attach(professor).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorExists(professor.Id))
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

        public async Task DeleteProfessor(int? id)
        {
            var professor = await _context.Professors.FindAsync(id);

            if (professor != null)
            {
                _context.Professors.Remove(professor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ArchiveProfessor(Boolean archive, int id)
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE Professors set Archived = {0} where Id = {1}", archive, id);
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professors.Any(e => e.Id == id);
        }
    }
}
