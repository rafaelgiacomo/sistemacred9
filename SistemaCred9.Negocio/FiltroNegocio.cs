using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using System.Collections.Generic;

namespace SistemaCred9.Negocio
{
    public class FiltroNegocio
    {
        private readonly UnitOfWork _unitOfWork;

        public FiltroNegocio(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Adicionar(Filtro entidade)
        {
            _unitOfWork.Filtro.Adicionar(entidade);
            _unitOfWork.Salvar();
        }

        public void Atualizar(Filtro entidade)
        {
            _unitOfWork.Filtro.Atualizar(entidade);
            _unitOfWork.Salvar();
        }

        public void Deletar(int id)
        {
            _unitOfWork.Filtro.Deletar(id);
            _unitOfWork.Salvar();
        }

        public Filtro SelecionarPorId(int id)
        {
            return _unitOfWork.Filtro.Obter(id);
        }

        public List<Filtro> ListarTodos()
        {
            return _unitOfWork.Filtro.Listar(x => x.Id == x.Id);
        }
    }
}
