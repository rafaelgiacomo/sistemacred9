using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using System.Collections.Generic;

namespace SistemaCred9.Negocio
{
    public class FiltroBancoNegocio
    {
        private readonly UnitOfWork _unitOfWork;

        public FiltroBancoNegocio(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Adicionar(FiltroBanco entidade)
        {
            _unitOfWork.FiltroBanco.Adicionar(entidade);
            _unitOfWork.Salvar();
        }

        public void Atualizar(FiltroBanco entidade)
        {
            _unitOfWork.FiltroBanco.Atualizar(entidade);
            _unitOfWork.Salvar();
        }

        public void Deletar(int id)
        {
            _unitOfWork.FiltroBanco.Deletar(id);
            _unitOfWork.Salvar();
        }

        public FiltroBanco SelecionarPorId(int id)
        {
            return _unitOfWork.FiltroBanco.Obter(id);
        }

        public List<FiltroBanco> ListarTodos()
        {
            return _unitOfWork.FiltroBanco.Listar(x => x.Id == x.Id);
        }
    }
}
