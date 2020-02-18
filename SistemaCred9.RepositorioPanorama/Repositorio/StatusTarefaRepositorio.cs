using MySql.Data.MySqlClient;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class StatusTarefaRepositorio
    {
        private readonly Context _context;

        public StatusTarefaRepositorio(Context context)
        {
            _context = context;
        }

        public StatusTarefa Selecionar(int id)
        {
            try
            {
                StatusTarefa entidade = null;
                string sql = "select id, descricao from status_proposta where ativo and id = @id";

                sql = sql.Replace("@id", id.ToString());

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
                return null;
            }
        }

        public List<StatusTarefa> ListarStatusTarefas(int tarefaId)
        {
            try
            {
                List<StatusTarefa> beneficios = new List<StatusTarefa>();
                string sql = @"select st.id, st.descricao from tarefa_status tf, status_proposta st 
	                            where tarefa_id = @tarefaId and tf.status_proposta_id = st.id and st.ativo 
	                            and tf.ativo group by st.id order by st.descricao;";

                sql = sql.Replace("@tarefaId", tarefaId.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                while (reader.Read())
                {
                    var ben = ReaderParaObjeto(reader);

                    if (ben != null)
                    {
                        beneficios.Add(ben);
                    }
                }

                reader.Close();

                return beneficios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<StatusTarefa> ListarStatusTarefasDiposniveisParaMudar(int tarefaId, int statusTarefaId)
        {
            try
            {
                List<StatusTarefa> beneficios = new List<StatusTarefa>();
                string sql = @"select t.id, t.id_ciclo, t.tarefa_status_id, t.status_proposta_id, s.descricao, s.cor 
	                            from tarefa_status t, status_proposta s
	                            where t.tarefa_id = @tarefaId and t.status_proposta_id=s.id
	                            and t.tarefa_status_id in 
                                (
		                            select t.id
		                            from tarefa_status t, status_proposta s
		                            where t.tarefa_id = @tarefaId and t.status_proposta_id=s.id
		                            and t.status_proposta_id = @statusTarefaId
	                            )
	                            order by t.status_proposta_id";

                sql = sql.Replace("@tarefaId", tarefaId.ToString());
                sql = sql.Replace("@statusTarefaId", statusTarefaId.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                while (reader.Read())
                {
                    var ben = ReaderParaObjeto2(reader);

                    if (ben != null)
                    {
                        beneficios.Add(ben);
                    }
                }

                reader.Close();

                return beneficios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private StatusTarefa ReaderParaObjeto(MySqlDataReader reader)
        {
            var entidade = new StatusTarefa();

            entidade.Id = int.Parse(reader["id"].ToString());
            entidade.Descricao = reader["descricao"].ToString();

            return entidade;
        }

        private StatusTarefa ReaderParaObjeto2(MySqlDataReader reader)
        {
            var entidade = new StatusTarefa();

            entidade.Id = int.Parse(reader["id"].ToString());
            entidade.Descricao = reader["descricao"].ToString();
            entidade.Cor = reader["cor"].ToString();

            return entidade;
        }
    }
}
