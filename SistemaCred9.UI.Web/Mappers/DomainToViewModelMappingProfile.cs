using AutoMapper;
using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Usuario;

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
            Mapper.CreateMap<Usuario, UsuarioViewModel>();
            Mapper.CreateMap<Usuario, TrocarSenhaViewModel>();
        }

    }
}