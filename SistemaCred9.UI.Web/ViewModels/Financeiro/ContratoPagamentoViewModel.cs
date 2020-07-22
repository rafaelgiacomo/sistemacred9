using System;

namespace SistemaCred9.Web.UI.ViewModels.Financeiro
{
    public class ContratoPagamentoViewModel
    {
        public int Id { get; set; }
        public int Contrato { get; set; }
        public string Cpf { get; set; }
        public string NomeCliente { get; set; }
        public string Produto { get; set; }
        public string Tabela { get; set; }
        public string Banco { get; set; }
        public string PercentualComissao { get; set; }
        public string ValorComissao { get; set; }
        public string ValorEmprestimo { get; set; }
        public string DataComissao { get; set; }
        public DateTime? DataImportacao { get; set; }
        public string TipoPlanilha { get; set; }
        public string NomeArquivo { get; set; }
    }
}