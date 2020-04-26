using SistemaCred9.Repositorio.Repositorios.Interfaces;
using System;
using System.Threading.Tasks;

namespace SistemaCred9.Repositorio.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Salvar();
        Task<int> SalvarAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        #region Repositorios

        IRoleRepositorio Role { get; }
        IUsuarioRepositorio Usuario { get; }
        IVendaRepositorio Venda { get; }
        IVendaStatusHistoricoRepositorio VendaStatusHistorico { get; }
        IFiltroRepositorio Filtro { get; }
        IFiltroEspecieRepositorio FiltroEspecie { get; }
        IFiltroBancoRepositorio FiltroBanco { get; }
        IContratoRelatorioRepositorio ContratoRelatorio { get; }
        ITabelaComissaoRepositorio TabelaComissao { get; }

        #endregion
    }
}
