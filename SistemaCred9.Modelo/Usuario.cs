using SistemaCred9.Modelo.Panorama;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SistemaCred9.Modelo
{
    public class Usuario : EntitadeBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public int TipoUsuarioId { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Venda> Vendas { get; set; }
        public virtual ICollection<Contrato> Contratos { get; set; }

        private readonly Hash _hash = new Hash(SHA512.Create());

        public Usuario()
        {
            Roles = new List<Role>();
            Vendas = new List<Venda>();
        }

        #region Outros metodos

        public void DefinirSenha(string senha)
        {
            Senha = _hash.CriptografarSenha(senha);
        }

        public bool VerificarSenha(string senhaDigitada, string senhaCadastrada)
        {
            return _hash.VerificarSenha(senhaDigitada, senhaCadastrada);
        }

        #endregion
    }
}
