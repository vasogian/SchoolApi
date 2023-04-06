using AutoMapper;

namespace SchoolApi.Profiles
{
    public class StudentProfile : Profile
    {
      public StudentProfile()
        {
            CreateMap<Models.Student,ViewModels.CreateOrUpdateStudentViewModel>();
            CreateMap<ViewModels.CreateOrUpdateStudentViewModel, Models.Student>();         
        }
    }
}
