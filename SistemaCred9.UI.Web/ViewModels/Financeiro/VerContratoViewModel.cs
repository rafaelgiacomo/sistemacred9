using SistemaCred9.Web.UI.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCred9.Web.UI.ViewModels.Financeiro
{
    public class VerContratoViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int Contrato { get; set; }
        public string Cpf { get; set; }
        public string NomeCliente { get; set; }
        public string NomeAssessor { get; set; }
        public string Tabela { get; set; }
        public string Banco { get; set; }
        public string BancoCredor { get; set; }
        public float PercentualComissao { get; set; }
        public float ValorCalculo { get; set; }
        public float ValorEmprestimo { get; set; }
        public string TarefaExecucaoStatus { get; set; }
        public string DataLancamento { get; set; }
        public int TabelaComissaoId { get; set; }

        public List<ContratoPagamentoViewModel> ListaContratoPagamento { get; set; }

        public VerContratoViewModel(List<ContratoPagamentoViewModel> listaContratoPagamento)
        {
            ListaContratoPagamento = listaContratoPagamento;
        }

        public VerContratoViewModel()
        {
            ListaContratoPagamento = new List<ContratoPagamentoViewModel>();
        }
    }
}