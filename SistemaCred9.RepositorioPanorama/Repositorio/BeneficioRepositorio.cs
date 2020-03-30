using MySql.Data.MySqlClient;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class BeneficioRepositorio
    {
        private readonly Context _context;
        private readonly ILogger _log;

        #region Parametros
        public const string ESPECIE = "especie";
        public const string ID = "id";
        public const string P_CLIENTE_ID = "MinDataNascimento";
        #endregion

        public BeneficioRepositorio(Context context, ILogger log)
        {
            _context = context;
            _log = log;
        }

        public List<Beneficio> ConsultaBeneficioPorClienteId(int idCliente)
        {
            try
            {
                string sql = "select b.id, b.especie as especie from beneficio b where b.cliente_id = @IdCliente";
                List<Beneficio> beneficios = new List<Beneficio>();

                sql = sql.Replace("@IdCliente", idCliente.ToString());

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

        private Beneficio ReaderParaObjeto(MySqlDataReader reader)
        {
            try
            {
                var entidade = new Beneficio();

                var especie = reader[ESPECIE].ToString();

                entidade.Id = int.Parse(reader[ID].ToString());

                if (!string.IsNullOrEmpty(especie))
                {
                    entidade.Especie = int.Parse(especie);
                }

                return entidade;
            }
            catch (Exception ex)
            {
                //_log.PulaLinhas(1);
                //_log.EscreveLinha("Erro ao converter Reader para Objeto");
                //_log.EscreveLinha("Classe BeneficioRepositorio");
                //_log.EscreveLinha(string.Format("Beneficio Id: {0} - Especie: {1}", reader[ID], reader[ESPECIE]));
                //_log.EscreveLinha("Mensagem do erro: " + ex.Message);
                //_log.PulaLinhas(1);
                return null;
            }
        }
    }
}
