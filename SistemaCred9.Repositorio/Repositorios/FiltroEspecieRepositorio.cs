using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.Base;
using SistemaCred9.Repositorio.Repositorios.Interfaces;

namespace SistemaCred9.Repositorio.Repositorios
{
    public class FiltroEspecieRepositorio : RepositorioBase<FiltroEspecie>, IFiltroEspecieRepositorio
    {
        public FiltroEspecieRepositorio(Cred9DbContext context) : base(context) { }
    }
}
