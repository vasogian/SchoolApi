using AutoMapper;

namespace SchoolApi.Profiles
{
    public class StudentProfile : Profile
    {
      public StudentProfile()
        {
            CreateMap<ViewModels.StudentViewModel, Models.Student>();
            CreateMap<Models.Student, ViewModels.StudentViewModel>();
            CreateMap<Models.Student,ViewModels.CreateOrUpdateStudentViewModel>();
            CreateMap<ViewModels.CreateOrUpdateStudentViewModel, Models.Student>();         
        }
    }
}
