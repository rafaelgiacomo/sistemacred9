using MySql.Data.MySqlClient;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class ContratoRepositorio
    {
        private readonly Context _context;

        #region Parametros
        public const string CLIENTE_ID = "cliente_id";
        public const string CONTRATO = "contrato";
        public const string DATA_LANCAMENTO = "data_lancamento";
        #endregion

        public ContratoRepositorio(Context context)
        {
            _context = context;
        }

        public List<Contrato> ListaContratosPorStatus(int tarefaId, int statusTarefaId)
        {
            try
            {
                string sql = @"select cliente_id, contrato, datalancamento from emprestimo where id in
	                            (select relacao_id from tarefa_execucao where tarefa_id = @tarefaId and tarefa_status_id in 
	                            (select id from tarefa_status where tarefa_id = @tarefaId and status_proposta_id = @statusTarefaId))";
                List<Contrato> lista = new List<Contrato>();

                sql = sql.Replace("@tarefaId", tarefaId.ToString());
                sql = sql.Replace("@statusTarefaId", statusTarefaId.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                while (reader.Read())
                {
                    var ben = ReaderParaObjeto(reader);

                    if (ben != null)
                    {
                        lista.Add(ben);
                    }
                }

                reader.Close();

                foreach (var item in lista)
                {
                    BuscaTarefaExecucaoId(item);
                }

                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void BuscaTarefaExecucaoId(Contrato contrato)
        {
            try
            {
                string sql = @"select id from tarefa_execucao where relacao='emprestimo' and relacao_id in(select id from emprestimo e where e.cliente_id=@clienteId and e.contrato=@contratoId)";
                List<Contrato> lista = new List<Contrato>();

                sql = sql.Replace("@clienteId", contrato.ClienteId.ToString());
                sql = sql.Replace("@contratoId", contrato.NumContrato.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                if (reader.Read())
                {
                    contrato.StatusTarefaExecucaoId = int.Parse(reader[0].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public int QtdContratosPorStatus(int tarefaId, int statusTarefaId)
        {
            try
            {
                int qtd = 0;

                string sql = @"select count(*) from emprestimo where id in
	                            (select relacao_id from tarefa_execucao where tarefa_id = @tarefaId and tarefa_status_id in 
	                            (select id from tarefa_status where tarefa_id = @tarefaId and status_proposta_id = @statusTarefaId))";

                sql = sql.Replace("@tarefaId", tarefaId.ToString());
                sql = sql.Replace("@statusTarefaId", statusTarefaId.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                while (reader.Read())
                {
                    qtd = int.Parse(reader[0].ToString());
                }

                reader.Close();

                return qtd;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private Contrato ReaderParaObjeto(MySqlDataReader reader)
        {
            var entidade = new Contrato();

            entidade.ClienteId = int.Parse(reader["cliente_id"].ToString());

            var contrato = reader["contrato"].ToString();
            var dataLancamento = reader["datalancamento"].ToString();

            if (!string.IsNullOrEmpty(contrato))
            {
                entidade.NumContrato = int.Parse(string.Join("", System.Text.RegularExpressions.Regex.Split(contrato, @"[^\d]")));
            }

            if (!string.IsNullOrEmpty(dataLancamento))
            {
                entidade.DataLancamento = DateTime.Parse(dataLancamento);
            }

            return entidade;
        }
    }
}
