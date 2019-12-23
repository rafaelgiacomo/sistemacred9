using SistemaCred9.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCred9.Negocio
{
    public class VendaNegocio
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendaNegocio(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
}
