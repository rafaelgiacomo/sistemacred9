using MySql.Data.MySqlClient;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class AgrupamentoRepositorio
    {
        private readonly Context _context;
        private readonly ILogger _log;

        #region Parametros
        public const string ID = "id";
        public const string DESCRICAO = "descricao";
        public const string CPF = "cpf";
        public const string P_MIN_DATA_NASCIMENTO = "MinDataNascimento";
        public const string P_BANCO = "Banco";
        public const string P_PRAZO = "Prazo";
        public const string P_MIN_PARCELAS_ABERTO = "MinParcelasAberto";
        public const string P_MAX_PARCELAS_ABERTO = "MaxParcelasAberto";
        public const string P_MIN_VALOR_PARCELA = "MinValorParcela";
        public const string P_COEFICIENTE = "Coeficiente";
        public const string P_MIN_LIQUIDO = "MinLiquido";
        #endregion

        public AgrupamentoRepositorio(Context context, ILogger log)
        {
            _context = context;
            _log = log;
        }

        public void AdicionarAgrupamento(Agrupamento entidade)
        {
            try
            {
                string sql = "insert into agrupamento (descricao, ativo, cor) values ('@Descricao', @Ativo, '@Cor')";

                sql = sql.Replace("@Descricao", entidade.Descricao);
                sql = sql.Replace("@Ativo", 1.ToString());
                sql = sql.Replace("@Cor", entidade.Cor);

                _context.ExecuteSqlCommandNoReturn(sql);

                sql = "SELECT LAST_INSERT_ID() as id";

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                if (reader.Read())
                {
                    entidade.Id = int.Parse(reader["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                //_log.EscreveLinha("Erro ao adicionar o agrupamento ( " + entidade.Descricao + " )");
                //_log.EscreveLinha("Classe AgrupamentoRepositorio - Método AdicionarAgrupamento(Agrupamento entidade)");
                //_log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public void AdicionarClienteNoAgrupamento(int idAgrupamento, int idCliente)
        {
            try
            {
                string sql = "insert into agrupamento_cliente (cliente_id, agrupamento_id, data, tipo) values (@IdCliente, @IdAgrupamento, now(), 'robo')";

                sql = sql.Replace("@IdCliente", idCliente.ToString());
                sql = sql.Replace("@IdAgrupamento", idAgrupamento.ToString());

                _context.ExecuteSqlCommandNoReturn(sql);
            }
            catch (Exception ex)
            {
                //_log.EscreveLinha("Erro ao adicionar o cliente ( " + idCliente + " ) no agrupamento ( " + idAgrupamento + " )");
                //_log.EscreveLinha("Classe AgrupamentoRepositorio - Método AdicionarClienteNoAgrupamento(int idAgrupamento, int idCliente)");
                //_log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public Agrupamento ConsultaAgrupamento(int agrupamentoId)
        {
            try
            {
                string sql = "select * from agrupamento where id = @AgrupamentoId";
                Agrupamento agp = null;

                sql = sql.Replace("@AgrupamentoId", agrupamentoId.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                if (reader.Read())
                {
                    agp = ReaderParaObjeto(reader);
                }

                return agp;
            }
            catch (Exception ex)
            {
                //_log.EscreveLinha("Erro ao consultar buscar agrupamento ( " + agrupamentoId + " )");
                //_log.EscreveLinha("Classe AgrupamentoRepositorio - Método ConsultaAgrupamento(int agrupamentoId)");
                //_log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return null;
            }
        }

        public Agrupamento Obter(int id)
        {
            try
            {
                var agp = new Agrupamento();
                string sql = "select * from agrupamento where id = @id";

                sql = sql.Replace("@id", id.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                if (reader.Read())
                {
                    agp = ReaderParaObjeto(reader);
                }

                return agp;
            }
            catch
            {
                return null;
            }
        }

        public List<Agrupamento> ListaAgrupamentos()
        {
            try
            {
                var listaRetorno = new List<Agrupamento>();
                string sql = "select * from agrupamento where ativo = 1";

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                while (reader.Read())
                {
                    var agp = ReaderParaObjeto(reader);

                    listaRetorno.Add(agp);
                }

                return listaRetorno;
            }
            catch
            {
                return new List<Agrupamento>();
            }
        }

        public bool ConsultaClientePresente(int idCliente, int idAgrupamento)
        {
            try
            {
                string sql = "select cliente_id from agrupamento_cliente where" +
                    " cliente_id = @IdCliente and agrupamento_id = @IdAgrupamento";
                bool retorno = false;

                sql = sql.Replace("@IdCliente", idCliente.ToString());
                sql = sql.Replace("@IdAgrupamento", idAgrupamento.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                if (reader.Read())
                {
                    var id = int.Parse(reader["cliente_id"].ToString());

                    if (id == idCliente)
                        retorno = true;
                }

                reader.Close();

                return retorno;
            }
            catch (Exception ex)
            {
                //_log.EscreveLinha("Erro ao consultar se o cliente ( " + idCliente + " ) está presente no agrupamento ( " + idAgrupamento + " )");
                //_log.EscreveLinha("Classe AgrupamentoRepositorio - Método ConsultaClientePresente(int idCliente, int idAgrupamento)");
                //_log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return false;
            }
        }

        private Agrupamento ReaderParaObjeto(MySqlDataReader reader)
        {
            try
            {
                var entidade = new Agrupamento();

                entidade.Descricao = reader[DESCRICAO].ToString();

                var id = reader[ID].ToString();

                if (!string.IsNullOrEmpty(id))
                    entidade.Id = int.Parse(id);

                return entidade;
            }
            catch (Exception ex)
            {
                //_log.EscreveLinha("Erro ao converter Reader para objeto");
                //_log.EscreveLinha("Classe AgrupamentoRepositorio");
                //_log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return null;
            }
        }

    }
}
