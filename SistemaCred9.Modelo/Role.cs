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
		    Operador, BackOffice, Coordenador
        };

        public const string Operador = "OPERADOR";
        public const string BackOffice = "BACKOFFICE";
        public const string Coordenador = "COORDENADOR";
    }
}
