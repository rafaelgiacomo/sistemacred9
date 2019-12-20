using SistemaCred9.Repositorio.UnitOfWork;

namespace SistemaCred9.Negocio
{
    public class UsuarioNegocio
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioNegocio(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
