using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCred9.Negocio
{
    public class UsuarioNegocio
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioNegocio(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Adicionar(Usuario entidade)
        {
            //entidade.Roles.Add(_unitOfWork.Role.Obter(entidade.TipoUsuarioId));
            _unitOfWork.Usuario.Adicionar(entidade);
            _unitOfWork.Salvar();
        }

        public void Atualizar(Usuario entidade)
        {
            _unitOfWork.Usuario.Atualizar(entidade);
            _unitOfWork.Salvar();
        }

        public void Deletar(int id)
        {
            _unitOfWork.Usuario.Deletar(id);
            _unitOfWork.Salvar();
        }

        public Usuario SelecionarPorId(int id)
        {
            return _unitOfWork.Usuario.Obter(id);
        }

        public Usuario SelecionarPorLogin(string login)
        {
            return _unitOfWork.Usuario.Listar(x => x.NomeUsuario.Equals(login)).FirstOrDefault();
        }

        public List<Usuario> ListarTodos()
        {
            return _unitOfWork.Usuario.Listar(x => x.Id == x.Id);
        }
    }
}
