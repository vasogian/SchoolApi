using AutoMapper;

namespace SchoolApi.Profiles
{
    public class ProfessorProfile : Profile
    {
        public ProfessorProfile()
        {
            CreateMap<ViewModels.CreateOrUpdateProfessorViewModel, Models.Professor>();
            CreateMap<Models.Professor, ViewModels.CreateOrUpdateProfessorViewModel>();
        }
        
    }
}
