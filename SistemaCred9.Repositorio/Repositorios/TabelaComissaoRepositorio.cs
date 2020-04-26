using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.Base;
using SistemaCred9.Repositorio.Repositorios.Interfaces;

namespace SistemaCred9.Repositorio.Repositorios
{
    public class TabelaComissaoRepositorio : RepositorioBase<TabelaComissao>, ITabelaComissaoRepositorio
    {
        public TabelaComissaoRepositorio(Cred9DbContext context) : base(context) { }
    }
}
