using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_Distribution_System.ExcelModels;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;

namespace Courses_Distribution_System.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseDetailsViewModel>()
                .ForMember(d=> d.SectionsCount, opt=> opt.MapFrom(src=> src.Sections.Count));
            CreateMap<Course, CoursesListViewModel>();
            CreateMap<Course, CourseEditViewModel>();
            CreateMap<CourseEditViewModel, Course>();
            CreateMap<Course, ExcelByCourse>()
                .ForMember(d=> d.TotalHours, opt=> opt.MapFrom(src=> src.Sections.Sum(s=> s.CourseHours+s.TdHours+s.TpHours)));
            CreateMap<Section, ExcelSectionsByCourse>()
                .ForMember(d => d.NameProf, opt => opt.MapFrom(src => src.Professor.FirstName + " " + src.Professor.LastName))
                .ForMember(d => d.Contract, opt => opt.MapFrom(src => src.Professor.Contract));
            CreateMap<Section, ExcelSectionsByProf>()
                .ForMember(d => d.Code, opt => opt.MapFrom(src => src.Course.Code))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Course.Description))
                .ForMember(d => d.Language, opt => opt.MapFrom(src => src.Course.Language))
                .ForMember(d=> d.Semester, opt=> opt.MapFrom(src=> src.Course.Semester));
            CreateMap<Professor, BaseProfessorsVM>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src =>src.FirstName+" "+ src.LastName));
            CreateMap<Professor, ProfessorsListViewModel>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<Professor, ExcelByProf>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(d=> d.TotalHours, opt=> opt.MapFrom(src=> src.Sections.Sum(s=> s.CourseHours+s.TdHours+s.TpHours)));
            CreateMap<Department, DepartmentCreateVM>();
        }
    }
}
