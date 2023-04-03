using AutoMapper;

namespace SchoolApi.Profiles
{
    public class StudentProfile : Profile
    {
      public StudentProfile()
        {
            CreateMap<Models.Student,ViewModels.StudentViewModel>();
            CreateMap<ViewModels.CreateOrUpdateStudentViewModel, Models.Student>();         
        }
    }
}
