using MySql.Data.MySqlClient;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class TelefoneRepositorio
    {
        private readonly Context _context;
        private readonly ILogger _log;

        #region Parametros
        public const string ID = "id";
        public const string DDD = "ddd";
        public const string NUMERO = "numero";
        public const string CLIENTE_ID = "cliente_id";
        public const string DATA = "data";
        public const string DATA_ACERTO = "data_acerto";
        public const string ORIGEM = "origem";
        #endregion

        public TelefoneRepositorio(Context context, ILogger log)
        {
            _context = context;
            _log = log;
        }

        public List<Telefone> ListarTelefonesDeCliente(int clienteId)
        {
            try
            {
                List<Telefone> lista = new List<Telefone>();

                string sql = "select * from telefone where cliente_id = @ClienteId";

                sql = sql.Replace("@ClienteId", clienteId.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                while (reader.Read())
                {
                    var entidade = ReaderParaObjeto(reader);

                    lista.Add(entidade);
                }

                reader.Close();

                return lista;
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao listar telefones do cliente ( " + clienteId + " )");
                _log.EscreveLinha("Classe ClienteRepositorio - Método ListarTelefonesDeCliente(int clienteId)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return new List<Telefone>();
            }
        }

        private Telefone ReaderParaObjeto(MySqlDataReader reader)
        {
            try
            {
                var entidade = new Telefone();

                entidade.Id = int.Parse(reader[ID].ToString());
                entidade.ClienteId = int.Parse(reader[CLIENTE_ID].ToString());
                entidade.Ddd = reader[DDD].ToString().Trim();
                entidade.Numero = reader[NUMERO].ToString().Trim();

                return entidade;
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao converter Reader para Objeto");
                _log.EscreveLinha("Classe ClienteRepositorio");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return null;
            }
        }

    }
}
