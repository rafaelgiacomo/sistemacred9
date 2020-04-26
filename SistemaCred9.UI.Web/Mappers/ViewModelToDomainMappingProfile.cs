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
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<UsuarioViewModel, Usuario>();
            CreateMap<TrocarSenhaViewModel, Usuario>();
            CreateMap<VendaViewModel, Venda>();
            CreateMap<VendaStatusHistoricoViewModel, VendaStatusHistorico>();
            CreateMap<EspecieViewModel, FiltroEspecie>();
            CreateMap<BancoViewModel, FiltroBanco>();
            CreateMap<ContratoRelatorioViewModel, Filtro>();
            CreateMap<TabelaComissaoViewModel, TabelaComissao>();
            CreateMap<ContratoRelatorioViewModel, ContratoRelatorio>();
        }

    }
}