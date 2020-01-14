using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class VendaStatusModelo
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public const int AGUARDANDO_DOCUMENTO_ID = 1;
        public const string AGUARDANDO_DOCUMENTO = "Aguardando Documento";

        public const int DOCUMENTO_PENDENTE_ID = 2;
        public const string DOCUMENTO_PENDENTE = "Documento Pendente";

        public const int CANCELADO_FALTA_DOCUMENTO_ID = 3;
        public const string CANCELADO_FALTA_DOCUMENTO = "Cancelado Falta Documento";

        public const int DOCUMENTO_OK_ID = 4;
        public const string DOCUMENTO_OK = "Documento OK";

        public const int AGUARDANDO_HISCON_ID = 5;
        public const string AGUARDANDO_HISCON = "Aguardando Hiscon";

        public const int HISCON_OK_ID = 6;
        public const string HISCON_OK = "Hiscon OK";

        public const int AGUARDANDO_CALCULO_ID = 7;
        public const string AGUARDANDO_CALCULO = "Aguardando Calculo";

        public const int CANCELADO_POR_CALCULO_ID = 8;
        public const string CANCELADO_POR_CALCULO = "Cancelado Por Calculo";

        public const int CALCULO_OK_ID = 9;
        public const string CALCULO_OK = "Calculo OK";

        public const int REPASSANDO_PROPOSTA_ID = 10;
        public const string REPASSANDO_PROPOSTA = "Repassando Proposta";

        public const int CANCELADO_REPASSE_PROPOSTA_ID = 11;
        public const string CANCELADO_REPASSE_PROPOSTA = "Cancelado Repasse Proposta";

        public const int POS_VENDA_ID = 12;
        public const string POS_VENDA = "Pósa Venda";

        public const int POS_VENDA_OK_ID = 13;
        public const string POS_VENDA_OK = "Pós Venda OK";

        public const int REGISTRO_PROPOSTA_ID = 14;
        public const string REGISTRO_PROPOSTA = "Registro Proposta";

        public const int PROPOSTA_REGISTRADA_ID = 15;
        public const string PROPOSTA_REGISTRADA = "Proposta Registrada";

        public static List<TipoUsuarioModelo> ListarTodos()
        {
            var lista = new List<TipoUsuarioModelo>();

            var pr1 = new TipoUsuarioModelo() { Id = AGUARDANDO_DOCUMENTO_ID, Descricao = AGUARDANDO_DOCUMENTO };
            var pr2 = new TipoUsuarioModelo() { Id = DOCUMENTO_PENDENTE_ID, Descricao = DOCUMENTO_PENDENTE };
            var pr3 = new TipoUsuarioModelo() { Id = CANCELADO_FALTA_DOCUMENTO_ID, Descricao = CANCELADO_FALTA_DOCUMENTO };
            var pr4 = new TipoUsuarioModelo() { Id = DOCUMENTO_OK_ID, Descricao = DOCUMENTO_OK };
            var pr5 = new TipoUsuarioModelo() { Id = AGUARDANDO_HISCON_ID, Descricao = AGUARDANDO_HISCON };
            var pr6 = new TipoUsuarioModelo() { Id = HISCON_OK_ID, Descricao = HISCON_OK };
            var pr7 = new TipoUsuarioModelo() { Id = AGUARDANDO_CALCULO_ID, Descricao = AGUARDANDO_CALCULO };
            var pr8 = new TipoUsuarioModelo() { Id = CANCELADO_POR_CALCULO_ID, Descricao = CANCELADO_POR_CALCULO };
            var pr9 = new TipoUsuarioModelo() { Id = CALCULO_OK_ID, Descricao = CALCULO_OK };
            var pr10 = new TipoUsuarioModelo() { Id = REPASSANDO_PROPOSTA_ID, Descricao = REPASSANDO_PROPOSTA };
            var pr11 = new TipoUsuarioModelo() { Id = CANCELADO_REPASSE_PROPOSTA_ID, Descricao = CANCELADO_REPASSE_PROPOSTA };
            var pr12 = new TipoUsuarioModelo() { Id = POS_VENDA_ID, Descricao = POS_VENDA };
            var pr13 = new TipoUsuarioModelo() { Id = POS_VENDA_OK_ID, Descricao = POS_VENDA_OK };
            var pr14 = new TipoUsuarioModelo() { Id = REGISTRO_PROPOSTA_ID, Descricao = REGISTRO_PROPOSTA };
            var pr15 = new TipoUsuarioModelo() { Id = PROPOSTA_REGISTRADA_ID, Descricao = PROPOSTA_REGISTRADA };

            lista.Add(pr1);
            lista.Add(pr2);
            lista.Add(pr3);
            lista.Add(pr4);
            lista.Add(pr5);
            lista.Add(pr6);
            lista.Add(pr7);
            lista.Add(pr8);
            lista.Add(pr9);
            lista.Add(pr10);
            lista.Add(pr11);
            lista.Add(pr12);
            lista.Add(pr13);
            lista.Add(pr14);
            lista.Add(pr15);

            return lista;
        }
    }
}
