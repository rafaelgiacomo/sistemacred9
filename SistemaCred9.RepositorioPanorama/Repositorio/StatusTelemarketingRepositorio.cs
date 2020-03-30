using MySql.Data.MySqlClient;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using System;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class StatusTelemarketingRepositorio
    {
        private readonly Context _context;
        private readonly ILogger _log;

        #region Parametros
        public const string ID = "id";
        public const string DESCRICAO = "descricao";
        public const string DATA_CONTATO = "dataContato";
        public const string P_ID_CLIENTE = "IdCliente";
        #endregion

        public StatusTelemarketingRepositorio(Context context, ILogger log)
        {
            _context = context;
            _log = log;
        }

        public StatusTelemarketing ConsultaStatusDoCliente(int idCliente)
        {
            try
            {
                StatusTelemarketing entidade = null;

                string sql = "select st.id, st.descricao, t.datacontato from status_telemarketing st, telemarketing t where " +
                    "st.id = t.status_telemarketing_id and t.relacao_id = @IdCliente and t.datacontato = " +
                    "(select max(datacontato) from telemarketing where relacao_id = @IdCliente)";

                sql = sql.Replace("@IdCliente", idCliente.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                if (reader.Read())
                {
                    entidade = ReaderParaObjeto(reader);
                }

                reader.Close();

                return entidade;
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao consultar status do cliente ( " + idCliente + " )");
                _log.EscreveLinha("Classe StatusTelemarketingRepositorio - Método ConsultaStatusDoCliente(int idCliente)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return null;
            }
        }

        private StatusTelemarketing ReaderParaObjeto(MySqlDataReader reader)
        {
            try
            {
                var entidade = new StatusTelemarketing();

                entidade.Descricao = reader[DESCRICAO].ToString();
                entidade.Data = reader[DATA_CONTATO].ToString();

                var id = reader[ID].ToString();

                if (!string.IsNullOrEmpty(id))
                    entidade.Id = int.Parse(id);

                return entidade;
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao converter Reader para Objeto");
                _log.EscreveLinha("Classe StatusTelemarketingRepositorio");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return null;
            }
        }

    }
}
