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
            TAREFA_INDEX, TAREFA_ASSUMIR, TAREFA_DEVOLVER, TAREFA_MUDAR_STATUS,

            // Espécie
		    ESPECIE_INDEX, ESPECIE_ADICIONAR, ESPECIE_EDITAR, ESPECIE_EXCLUIR,

            // Banco
		    BANCO_INDEX, BANCO_ADICIONAR, BANCO_EDITAR, BANCO_EXCLUIR,

            // Filtro
		    FILTRO_INDEX, FILTRO_ADICIONAR, FILTRO_EDITAR, FILTRO_EXCLUIR,

            // Tabela Comissao
		    TABELA_COMISSAO_INDEX, TABELA_COMISSAO_ADICIONAR, TABELA_COMISSAO_EDITAR, TABELA_COMISSAO_EXCLUIR,

            // Contra Relatorio
		    CONTRATO_RELATORIO_INDEX, CONTRATO_RELATORIO_ADICIONAR, CONTRATO_RELATORIO_EDITAR, CONTRATO_RELATORIO_EXCLUIR
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
        public const string TAREFA_MUDAR_STATUS = "TAREFA_MUDAR_STATUS";

        //Especie
        public const string ESPECIE_INDEX = "ESPECIE_INDEX";
        public const string ESPECIE_ADICIONAR = "ESPECIE_ADICIONAR";
        public const string ESPECIE_EDITAR = "ESPECIE_EDITAR";
        public const string ESPECIE_EXCLUIR = "ESPECIE_EXCLUIR";

        //Banco
        public const string BANCO_INDEX = "BANCO_INDEX";
        public const string BANCO_ADICIONAR = "BANCO_ADICIONAR";
        public const string BANCO_EDITAR = "BANCO_EDITAR";
        public const string BANCO_EXCLUIR = "BANCO_EXCLUIR";

        //Filtro
        public const string FILTRO_INDEX = "FILTRO_INDEX";
        public const string FILTRO_ADICIONAR = "FILTRO_ADICIONAR";
        public const string FILTRO_EDITAR = "FILTRO_EDITAR";
        public const string FILTRO_EXCLUIR = "FILTRO_EXCLUIR";

        //Tabela Comissao
        public const string TABELA_COMISSAO_INDEX = "TABELA_COMISSAO_INDEX";
        public const string TABELA_COMISSAO_ADICIONAR = "TABELA_COMISSAO_ADICIONAR";
        public const string TABELA_COMISSAO_EDITAR = "TABELA_COMISSAO_EDITAR";
        public const string TABELA_COMISSAO_EXCLUIR = "TABELA_COMISSAO_EXCLUIR";

        //Contrato Relatorio
        public const string CONTRATO_RELATORIO_INDEX = "CONTRATO_RELATORIO_INDEX";
        public const string CONTRATO_RELATORIO_ADICIONAR = "CONTRATO_RELATORIO_ADICIONAR";
        public const string CONTRATO_RELATORIO_EDITAR = "CONTRATO_RELATORIO_EDITAR";
        public const string CONTRATO_RELATORIO_EXCLUIR = "CONTRATO_RELATORIO_EXCLUIR";

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
                    TAREFA_INDEX, TAREFA_ASSUMIR, TAREFA_DEVOLVER, TAREFA_MUDAR_STATUS
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
