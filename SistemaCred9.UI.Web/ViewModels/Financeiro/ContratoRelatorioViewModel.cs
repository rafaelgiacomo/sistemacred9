﻿using System;

namespace SistemaCred9.Web.UI.ViewModels.Financeiro
{
    public class ContratoRelatorioViewModel
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
        public DateTime DataLancamento { get; set; }
        public DateTime? DataImportacao { get; set; }
        public DateTime? DataCpc { get; set; }
        public int TabelaComissaoId { get; set; }
        public bool Selecionado { get; set; }

        public float PercentualComissaoCalculado { get; set; }
    }
}