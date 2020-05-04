using System;

namespace SistemaCred9.Core.Dto
{
    public class ContratoRelatorioDto
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
        public DateTime? DataLancamento { get; set; }
        public int? TabelaComissaoId { get; set; }

        public float PercentualComissaoCalculado { get; set; }
    }
}
