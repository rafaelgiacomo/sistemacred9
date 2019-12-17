using AutoMapper;

namespace SistemaCred9.Web.UI.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
 
        protected override void Configure()
        {
            //Mapper.CreateMap<CondominioViewModel, CondominioModel>();
            //Mapper.CreateMap<TorreViewModel, TorreModel>();
            //Mapper.CreateMap<ApartamentoViewModel, VagaModel>();
            //Mapper.CreateMap<SorteioViewModel, SorteioModel>();
            //Mapper.CreateMap<FaseSorteioViewModel, FaseSorteioModel>();
        }

    }
}