namespace SistemaCred9.Core.Dto
{
    public class ContratoRelatorioErroDto
    {
        public string Id { get; set; }
        public string Contrato { get; set; }
        public string Cpf { get; set; }
        public string NomeCliente { get; set; }
        public string NomeAssessor { get; set; }
        public string Produto { get; set; }
        public string Tabela { get; set; }
        public string Banco { get; set; }
        public string BancoCredor { get; set; }
        public string PercentualComissao { get; set; }
        public string ValorCalculo { get; set; }
        public string ValorEmprestimo { get; set; }
        public string TarefaExecucaoStatus { get; set; }
        public string DataLancamento { get; set; }
        public string DataComissao { get; set; }
        public string TabelaComissaoId { get; set; }
    }
}
