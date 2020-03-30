using MySql.Data.MySqlClient;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class EmprestimoRepositorio
    {
        private readonly Context _context;
        private readonly ILogger _log;

        #region Parametros
        public const string ID = "id";
        public const string BENEFICIO_ID = "beneficio_id";
        public const string BANCO = "banco";
        public const string PRAZO = "numero_parcelas";
        public const string PARCELAS_EM_ABERTO = "numero_parcelas_aberto";
        public const string VALOR_PARCELA = "valor_margem";
        public const string SALDO = "valor_refinanciamento";
        public const string P_CLIENTE_ID = "@ClienteId";
        #endregion

        public EmprestimoRepositorio(Context context, ILogger log)
        {
            _context = context;
            _log = log;
        }

        public List<Emprestimo> ConsultaEmprestimosPorBeneficioId(int idBeneficio)
        {
            try
            {
                List<Emprestimo> emprestimos = new List<Emprestimo>();
                string sql = "select c.id, c.banco, c.numero_parcelas, c.numero_parcelas_aberto, " +
                    "c.valor_margem, c.valor_refinanciamento from consignado c where c.beneficio_id = @IdBeneficio";

                sql = sql.Replace("@IdBeneficio", idBeneficio.ToString());

                var reader = _context.ExecuteSqlCommandWithReturn(sql);

                while (reader.Read())
                {
                    Emprestimo emprestimo = ReaderParaObjeto(reader);
                    emprestimo.BeneficioId = idBeneficio;

                    emprestimos.Add(emprestimo);
                }

                reader.Close();

                return emprestimos;
            }
            catch (Exception ex)
            {
                return new List<Emprestimo>();
            }
        }

        private Emprestimo ReaderParaObjeto(MySqlDataReader reader)
        {
            try
            {
                var entidade = new Emprestimo();

                var parcelasAberto = reader[PARCELAS_EM_ABERTO].ToString();
                var prazo = reader[PRAZO].ToString();
                var saldo = reader[SALDO].ToString();
                var valorParcela = reader[VALOR_PARCELA].ToString();

                entidade.Id = int.Parse(reader[ID].ToString());
                entidade.Banco = reader[BANCO].ToString();

                if (!string.IsNullOrEmpty(parcelasAberto))
                    entidade.ParcelasEmAberto = int.Parse(parcelasAberto);

                if (!string.IsNullOrEmpty(prazo))
                    entidade.Prazo = int.Parse(prazo);

                if (!string.IsNullOrEmpty(saldo))
                    entidade.Saldo = double.Parse(saldo);

                if (!string.IsNullOrEmpty(valorParcela))
                    entidade.ValorParcela = double.Parse(valorParcela);

                return entidade;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
