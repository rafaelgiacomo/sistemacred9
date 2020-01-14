using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo.Panorama;
using SistemaCred9.Repositorio.Base;
using SistemaCred9.Repositorio.Repositorios.Interfaces;

namespace SistemaCred9.Repositorio.Repositorios
{
    public class ContratoRepositorio : RepositorioBase<Contrato>, IContratoRepositorio
    {
        public ContratoRepositorio(Cred9DbContext context) : base(context) { }
    }
}
