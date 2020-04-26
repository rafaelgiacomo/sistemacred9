using AutoMapper;
using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Banco;
using SistemaCred9.Web.UI.ViewModels.Especie;
using SistemaCred9.Web.UI.ViewModels.Filtro;
using SistemaCred9.Web.UI.ViewModels.TabelaComissao;
using SistemaCred9.Web.UI.ViewModels.Usuario;
using SistemaCred9.Web.UI.ViewModels.Venda;

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
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<Usuario, TrocarSenhaViewModel>();
            CreateMap<Venda, VendaViewModel>();
            CreateMap<VendaStatusHistorico, VendaStatusHistoricoViewModel>();
            CreateMap<FiltroEspecie, EspecieViewModel>();
            CreateMap<FiltroBanco, BancoViewModel>();
            CreateMap<Filtro, ContratoRelatorioViewModel>();
            CreateMap<TabelaComissao, TabelaComissaoViewModel>();
            CreateMap<ContratoRelatorio, ContratoRelatorioViewModel>();
        }

    }
}