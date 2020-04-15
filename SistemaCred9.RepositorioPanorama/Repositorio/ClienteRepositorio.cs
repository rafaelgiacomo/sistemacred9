using MySql.Data.MySqlClient;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly Context _context;
        private readonly ILogger _log;

        #region Parametros
        public const string ID = "id";
        public const string CPF = "cpf";
        public const string NOME = "nome";
        public const string TELEFONE = "telefone";
        public const string CELULAR = "celular";
        public const string P_MIN_DATA_NASCIMENTO = "MinDataNascimento";
        public const string P_BANCO = "Banco";
        public const string P_PRAZO = "Prazo";
        public const string P_MIN_PARCELAS_ABERTO = "MinParcelasAberto";
        public const string P_MAX_PARCELAS_ABERTO = "MaxParcelasAberto";
        public const string P_MIN_VALOR_PARCELA = "MinValorParcela";
        public const string P_COEFICIENTE = "Coeficiente";
        public const string P_MIN_LIQUIDO = "MinLiquido";
        #endregion

        public ClienteRepositorio(Context context, ILogger log)
        {
            _context = context;
            _log = log;
        }

        public List<Cliente> ListarClientesDentroPerfil(FiltroBancoPanorama filtro, string dataNascimento, int limite)
        {
            try
            {
                List<Cliente> lista = new List<Cliente>();

                //string sql = "select distinct c.id from cliente c, consignado e, beneficio b where c.DATANASCIMENTO > '@MinDataNascimento' " +
                //    "and e.banco like '%@Banco%' and c.id = b.cliente_id and b.id = e.beneficio_id limit @LimiteRegistros";

                string sql = "select distinct c.id from cliente c, consignado e, beneficio b where c.DATANASCIMENTO > '@MinDataNascimento' " +
                    "and e.banco like '%@Banco%' and e.numero_parcelas = @Prazo and e.numero_parcelas_aberto between @MinParcelasAberto and @MaxParcelasAberto " +
                    "and e.valor_margem >= @MinValorParcela and c.id = b.cliente_id and b.id = e.beneficio_id limit @LimiteRegistros";

                sql = sql.Replace("@MinDataNascimento", dataNascimento);
                sql = sql.Replace("@Banco", filtro.Banco);
                sql = sql.Replace("@Prazo", filtro.Prazo.ToString());
                sql = sql.Replace("@MinParcelasAberto", filtro.MinParcelasEmAberto.ToString());
                sql = sql.Replace("@MaxParcelasAberto", filtro.MaxParcelasEmAberto.ToString());
                sql = sql.Replace("@MinValorParcela", filtro.MinParcela.ToString());
                sql = sql.Replace("@LimiteRegistros", limite.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                while (reader.Read())
                {
                    var entidade = ReaderParaObjeto(reader);

                    lista.Add(entidade);
                }

                reader.Close();

                return lista;
            }
            catch
            {
                throw;
            }
        }

        public List<Cliente> ListarClientesDeAgrupamento(int agrupamentoId)
        {
            try
            {
                List<Cliente> lista = new List<Cliente>();

                string sql = "select cliente_id as id from agrupamento_cliente where agrupamento_id = @AgrupamentoId order by cliente_id";

                sql = sql.Replace("@AgrupamentoId", agrupamentoId.ToString());

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
                _log.EscreveLinha("Erro ao listar clientes dentro do agrupamento ( " + agrupamentoId + " )");
                _log.EscreveLinha("Classe ClienteRepositorio - Método ListarClientesDeAgrupamento(int agrupamentoId)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return new List<Cliente>();
            }
        }

        public void BuscaContatoCliente(Cliente cliente)
        {
            try
            {
                string sql = "select ct.nome, c.cpf, ct.TELEFONE as telefone, ct.CELULAR as celular from contato ct, cliente c " +
                    "where c.CONTATO_ID = ct.id and c.Id = @ClienteId";

                sql = sql.Replace("@ClienteId", cliente.Id.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                if (reader.Read())
                {
                    cliente.Nome = reader[NOME].ToString();
                    cliente.Cpf = reader[CPF].ToString();
                    cliente.TelefoneContato = reader[TELEFONE].ToString();
                    cliente.CelularContato = reader[CELULAR].ToString();
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao buscar contato do cliente ( " + cliente.Id + " )");
                _log.EscreveLinha("Classe ClienteRepositorio - Método BuscaContatoCliente(Cliente cliente)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public void PreencherNomeECpfCliente(Cliente cliente)
        {
            try
            {
                string sql = "select cliente_id as id from agrupamento_cliente where agrupamento_id = @AgrupamentoId";
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao carregar informacoes do cliente ( " + cliente.Id + " )");
                _log.EscreveLinha("Classe ClienteRepositorio - Método PreencherNomeECpfCliente(Cliente cliente)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        private Cliente ReaderParaObjeto(MySqlDataReader reader)
        {
            try
            {
                var entidade = new Cliente();

                //entidade.Cpf = reader[CPF].ToString();

                var id = reader[ID].ToString();

                if (!string.IsNullOrEmpty(id))
                    entidade.Id = int.Parse(id);

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
