using MySql.Data.MySqlClient;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class TarefaRepositorio
    {
        private readonly Context _context;

        public TarefaRepositorio(Context context)
        {
            _context = context;
        }

        public Tarefa Selecionar(int id)
        {
            try
            {
                Tarefa entidade = null;
                string sql = "select id, descricao from tarefa where ativo and id = @id";

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

        public List<Tarefa> ListarTarefas()
        {
            try
            {
                List<Tarefa> beneficios = new List<Tarefa>();
                string sql = "select id, descricao from tarefa where ativo";

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

        private Tarefa ReaderParaObjeto(MySqlDataReader reader)
        {
            var entidade = new Tarefa();

            entidade.Id = int.Parse(reader["id"].ToString());
            entidade.Descricao = reader["descricao"].ToString();

            return entidade;
        }
    }
}
