using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using System.Collections.Generic;

namespace SistemaCred9.Negocio
{
    public class TabelaComissaoNegocio
    {
        private readonly UnitOfWork _unitOfWork;

        public TabelaComissaoNegocio(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Adicionar(TabelaComissao entidade)
        {
            _unitOfWork.TabelaComissao.Adicionar(entidade);
            _unitOfWork.Salvar();
        }

        public void Atualizar(TabelaComissao entidade)
        {
            _unitOfWork.TabelaComissao.Atualizar(entidade);
            _unitOfWork.Salvar();
        }

        public void Deletar(int id)
        {
            _unitOfWork.TabelaComissao.Deletar(id);
            _unitOfWork.Salvar();
        }

        public TabelaComissao SelecionarPorId(int id)
        {
            return _unitOfWork.TabelaComissao.Obter(id);
        }

        public List<TabelaComissao> SelecionarPorDescricao(string descricao)
        {
            return _unitOfWork.TabelaComissao.Listar(x => x.Descricao == descricao);
        }

        public List<TabelaComissao> ListarTodos()
        {
            return _unitOfWork.TabelaComissao.Listar(x => x.Id == x.Id);
        }
    }
}
