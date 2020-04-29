using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class TipoPlanilhaModelo
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public const int PANORAMA_ID = (int)TipoPlanilhaEnum.Panorama;
        public const string PANORAMA = "Panorama";

        public const int CORRETORA_ID = (int)TipoPlanilhaEnum.Corretora;
        public const string CORRETORA = "Corretora";

        public const int BANCO_ID = (int)TipoPlanilhaEnum.Banco;
        public const string BANCO = "Banco";

        public static List<TipoPlanilhaModelo> ListarTodos()
        {
            var lista = new List<TipoPlanilhaModelo>();

            var pr1 = new TipoPlanilhaModelo() { Id = PANORAMA_ID, Descricao = PANORAMA };
            var pr2 = new TipoPlanilhaModelo() { Id = CORRETORA_ID, Descricao = CORRETORA };
            var pr3 = new TipoPlanilhaModelo() { Id = BANCO_ID, Descricao = BANCO };

            lista.Add(pr1);
            lista.Add(pr2);
            lista.Add(pr3);

            return lista;
        }
    }
}
