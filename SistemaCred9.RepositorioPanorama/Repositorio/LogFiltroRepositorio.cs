using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.RepositorioPanorama.Repositorio
{
    public class LogFiltroRepositorio
    {
        private readonly Context _context;
        private readonly ILogger _log;

        #region Parametros
        #endregion

        public LogFiltroRepositorio(Context context, ILogger log)
        {
            _context = context;
            _log = log;
        }

        public void SalvarLog(LogFiltro logFiltro)
        {
            try
            {
                List<Emprestimo> emprestimos = new List<Emprestimo>();
                string sql = "insert into log_filtro(cliente_id, beneficio_id, agrupamento_id, valor_parcela, saldo, bruto, " +
                    "liquido, prazo, parcelas_aberto, banco, filtro_min_valor_parcela, filtro_min_liquido, filtro_parcelas_aberto, " +
                    "filtro_banco, filtro_prazo, observacao, filtrado) values (@ClienteId, @BeneficioId, @AgrupamentoId, " +
                    "@ValorParcela, @Saldo, @Bruto, @Liquido, @Prazo, @ParcelasAberto, '@Banco', @FMinParcela, @FMinLiquido, " +
                    "@FParcelas_Aberto, '@FiltroBanco', @FiltroPrazo, '@Observacao', @Filtrado)";

                sql = sql.Replace("@ClienteId", logFiltro.ClienteId.ToString());
                sql = sql.Replace("@BeneficioId", logFiltro.Emprestimo.BeneficioId.ToString());
                sql = sql.Replace("@AgrupamentoId", logFiltro.AgrupamentoId.ToString());
                sql = sql.Replace("@ValorParcela", logFiltro.Emprestimo.ValorParcela.ToString().Replace(",", "."));
                sql = sql.Replace("@Saldo", logFiltro.Emprestimo.Saldo.ToString().Replace(",", "."));
                sql = sql.Replace("@Bruto", logFiltro.Bruto.ToString().Replace(",", "."));
                sql = sql.Replace("@Liquido", logFiltro.Liquido.ToString().Replace(",", "."));
                sql = sql.Replace("@Prazo", logFiltro.Emprestimo.Prazo.ToString());
                sql = sql.Replace("@ParcelasAberto", logFiltro.Emprestimo.ParcelasEmAberto.ToString());
                sql = sql.Replace("@Banco", logFiltro.Emprestimo.Banco);
                sql = sql.Replace("@FMinParcela", logFiltro.FiltroBanco.MinParcela.ToString().Replace(",", "."));
                sql = sql.Replace("@FMinLiquido", logFiltro.FiltroBanco.MinLiquido.ToString().Replace(",", "."));
                sql = sql.Replace("@FParcelas_Aberto", logFiltro.FiltroBanco.MinParcelasEmAberto.ToString());
                sql = sql.Replace("@FiltroBanco", logFiltro.FiltroBanco.Banco.ToString());
                sql = sql.Replace("@FiltroPrazo", logFiltro.FiltroBanco.Prazo.ToString());
                sql = sql.Replace("@Observacao", string.Empty);
                sql = sql.Replace("@Filtrado", logFiltro.Filtrado.ToString());

                _context.ExecuteSqlCommandNoReturn(sql);
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao salvar log ( ClienteId: " + logFiltro.ClienteId + " )");
                _log.EscreveLinha("Classe LogFiltroRepositorio - Método SalvarLog(LogFiltroModel logFiltro)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }
    }
}
