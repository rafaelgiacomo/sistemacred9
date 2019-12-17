using AutoMapper;

namespace SistemaCred9.Web.UI.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<CondominioModel, CondominioViewModel>();
        }

    }
}