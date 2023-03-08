using AutoMapper;

namespace SchoolApi.Profiles
{
    public class ProfessorProfile : Profile
    {
        public ProfessorProfile()
        {
            CreateMap<ViewModels.CreateOrUpdateProfessorViewModel, Models.Professor>();
        }
        
    }
}
