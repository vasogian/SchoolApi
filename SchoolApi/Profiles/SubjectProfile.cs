using AutoMapper;

namespace SchoolApi.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Models.Subjects, ViewModels.SubjectViewModel>();
            CreateMap<ViewModels.CreateOrUpdateSubjectViewModel, Models.Subjects>();
        }
    }
}
