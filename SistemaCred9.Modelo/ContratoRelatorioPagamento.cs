using System;

namespace SistemaCred9.Modelo
{
    public class ContratoRelatorioPagamento : EntitadeBase
    {
        public int Contrato { get; set; }
        public string Cpf { get; set; }
        public string NomeCliente { get; set; }
        public string Produto { get; set; }
        public string Tabela { get; set; }
        public string Banco { get; set; }
        public float PercentualComissao { get; set; }
        public float ValorComissao { get; set; }
        public float ValorEmprestimo { get; set; }
        public DateTime? DataComissao { get; set; }
        public TipoPlanilhaEnum TipoPlanilha { get; set; }
        public string NomeArquivo { get; set; }
        public int? TabelaComissaoId { get; set; }
        public int? ContratoRelatorioId { get; set; }

        public virtual ContratoRelatorio ContratoRelatorio { get; set; }

    }
}
