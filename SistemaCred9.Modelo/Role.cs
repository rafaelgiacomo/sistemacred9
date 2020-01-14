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
            // Usuario
		    USUARIO_INDEX, USUARIO_ADICIONAR, USUARIO_EDITAR, USUARIO_EXCLUIR,

            // Venda 
            VENDA_INDEX, VENDA_ADICIONAR, VENDA_EDITAR, VENDA_MUDAR_STATUS, VENDA_EXCLUIR,

            //Tarefa
            TAREFA_INDEX, TAREFA_ASSUMIR, TAREFA_DEVOLVER
        };

        //Usuario
        public const string USUARIO_INDEX = "USUARIO_INDEX";
        public const string USUARIO_ADICIONAR = "USUARIO_ADICIONAR";
        public const string USUARIO_EDITAR = "USUARIO_EDITAR";
        public const string USUARIO_EXCLUIR = "USUARIO_EXCLUIR";

        //Venda
        public const string VENDA_INDEX = "VENDA_INDEX";
        public const string VENDA_ADICIONAR = "VENDA_ADICIONAR";
        public const string VENDA_EDITAR = "VENDA_EDITAR";
        public const string VENDA_MUDAR_STATUS = "VENDA_MUDAR_STATUS";
        public const string VENDA_EXCLUIR = "VENDA_EXCLUIR";

        //Tarefa
        public const string TAREFA_INDEX = "TAREFA_INDEX";
        public const string TAREFA_ASSUMIR = "TAREFA_ASSUMIR";
        public const string TAREFA_DEVOLVER = "TAREFA_DEVOLVER";

        public static string[] RolesPorTipoUsuario(TipoUsuarioEnum tipoUsuario)
        {
            if (TipoUsuarioEnum.Operador == tipoUsuario)
            {
                return new string[] 
                {
                    VENDA_INDEX, VENDA_ADICIONAR
                };
            }

            if (TipoUsuarioEnum.BackOffice == tipoUsuario)
            {
                return new string[] 
                {
                    VENDA_MUDAR_STATUS, VENDA_EDITAR
                };
            }

            if (TipoUsuarioEnum.Coordenador == tipoUsuario)
            {
                return new string[] 
                {
                    USUARIO_INDEX, USUARIO_ADICIONAR, USUARIO_EDITAR, USUARIO_EXCLUIR,
                    VENDA_INDEX, VENDA_ADICIONAR, VENDA_EDITAR, VENDA_EXCLUIR, VENDA_MUDAR_STATUS
                };
            }

            if (TipoUsuarioEnum.AdmInterna == tipoUsuario)
            {
                return new string[]
                {
                    TAREFA_INDEX, TAREFA_ASSUMIR, TAREFA_DEVOLVER
                };
            }

            if (TipoUsuarioEnum.Administrador == tipoUsuario)
            {
                return Roles;
            }

            return new string[] { };
        }
    }
}
