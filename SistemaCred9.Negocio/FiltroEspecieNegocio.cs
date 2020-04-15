using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using System.Collections.Generic;

namespace SistemaCred9.Negocio
{
    public class FiltroEspecieNegocio
    {
        private readonly UnitOfWork _unitOfWork;

        public FiltroEspecieNegocio(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Adicionar(FiltroEspecie entidade)
        {
            _unitOfWork.FiltroEspecie.Adicionar(entidade);
            _unitOfWork.Salvar();
        }

        public void Atualizar(FiltroEspecie entidade)
        {
            _unitOfWork.FiltroEspecie.Atualizar(entidade);
            _unitOfWork.Salvar();
        }

        public void Deletar(int id)
        {
            _unitOfWork.FiltroEspecie.Deletar(id);
            _unitOfWork.Salvar();
        }

        public FiltroEspecie SelecionarPorId(int id)
        {
            return _unitOfWork.FiltroEspecie.Obter(id);
        }

        public List<FiltroEspecie> ListarTodos()
        {
            return _unitOfWork.FiltroEspecie.Listar(x => x.Id == x.Id);
        }
    }
}
