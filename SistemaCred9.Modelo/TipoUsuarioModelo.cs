using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class TipoUsuarioModelo
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public const int OPERADOR_ID = 1;
        public const string OPERADOR = "Operador";
        public const int BACKOFFICE_ID = 2;
        public const string BACKOFFICE = "BackOffice";
        public const int COORDENADOR_ID = 3;
        public const string COORDENADOR = "Coordenador";
        public const int ADM_ID = 3;
        public const string ADM = "Administrador";

        public static List<TipoUsuarioModelo> ListarTodos()
        {
            var lista = new List<TipoUsuarioModelo>();

            var pr1 = new TipoUsuarioModelo() { Id = OPERADOR_ID, Descricao = OPERADOR };
            var pr2 = new TipoUsuarioModelo() { Id = BACKOFFICE_ID, Descricao = BACKOFFICE };
            var pr3 = new TipoUsuarioModelo() { Id = COORDENADOR_ID, Descricao = COORDENADOR };
            var pr4 = new TipoUsuarioModelo() { Id = ADM_ID, Descricao = ADM };

            lista.Add(pr1);
            lista.Add(pr2);
            lista.Add(pr3);
            lista.Add(pr4);

            return lista;
        }

    }
}
