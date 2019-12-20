using System.Collections.Generic;

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
    }
}
