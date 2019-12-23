using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class Role : EntitadeBase
    {
        #region Properties
        public string Descricao { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
        #endregion

        public static readonly string[] Roles =
        {
            // Operador
		    Operador, BackOffice, Coordenador, Administrador
        };

        public const string Operador = "Operador";
        public const string BackOffice = "BackOffice";
        public const string Coordenador = "Coordenador";
        public const string Administrador = "Administrador";
    }
}
