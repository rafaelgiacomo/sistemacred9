using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.Base;
using SistemaCred9.Repositorio.Repositorios.Interfaces;

namespace SistemaCred9.Repositorio.Repositorios
{
    public class VendaStatusHistoricoRepositorio : RepositorioBase<VendaStatusHistorico>, IVendaStatusHistoricoRepositorio
    {
        public VendaStatusHistoricoRepositorio(Cred9DbContext context) : base(context) { }
    }
}
