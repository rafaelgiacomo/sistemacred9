using SistemaCred9.Core.Dto;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo;
using System;
using System.Collections.Generic;

namespace SistemaCred9.Infra
{
    public class LeitorContratoBancoSafraArquivo : ILeitorArquivoContratoPagamento
    {
        private readonly ILeitorArquivo _leitorArquivo;
        public const int INDEX_DATA_COMISSAO = 8;
        public const int INDEX_TABELA = 10;
        public const int INDEX_PRODUTO = 09;
        public const int INDEX_PERCENTUAL_COMISSAO = 4;
        public const int INDEX_CONTRATO = 0;
        public const int INDEX_VALOR_COMISSAO = 3;
        public const int INDEX_VALOR_EMPRESTIMO = 1;
        public const string CHARACTER_NULO = "";

        private bool _ehFimArquivo;

        #region Propriedades

        public bool EhFimArquivo { get { return _ehFimArquivo; } }
        public List<ContratoRelatorioPagamento> Contratos { get; set; }
        public List<ContratoRelatorioErroDto> LinhasComErro { get; set; }

        #endregion

        public LeitorContratoBancoSafraArquivo(ILeitorArquivo leitorArquivo)
        {
            _leitorArquivo = leitorArquivo;
            Contratos = new List<ContratoRelatorioPagamento>();
            LinhasComErro = new List<ContratoRelatorioErroDto>();
        }

        public void PulaCabecalho()
        {
            _leitorArquivo.LeLinha();
        }

        public void LeConfigProximaLinha()
        {
            string[] linha = _leitorArquivo.LeLinha();

            try
            {
                if (linha != null
                && !string.IsNullOrEmpty(linha[INDEX_CONTRATO])
                && !CHARACTER_NULO.Equals(linha[INDEX_CONTRATO]))
                {
                    Contratos.Add(LeEntidade(linha));
                }
                else
                {
                    _ehFimArquivo = true;
                }
            }
            catch
            {
                LinhasComErro.Add(LeEntidadeComErro(linha));
            }
        }

        public ContratoRelatorioPagamento LeEntidade(string[] linha)
        {
            ContratoRelatorioPagamento entidade = new ContratoRelatorioPagamento();

            entidade.DataComissao = DateTime.Parse(linha[INDEX_DATA_COMISSAO]);
            entidade.ValorComissao = float.Parse(linha[INDEX_VALOR_COMISSAO].Replace("R$", "").Replace(" ", ""));
            entidade.ValorEmprestimo = float.Parse(linha[INDEX_VALOR_EMPRESTIMO].Replace("R$", "").Replace(" ", ""));
            entidade.Tabela = linha[INDEX_TABELA];
            entidade.Banco = "SAFRA";
            entidade.Produto = linha[INDEX_PRODUTO];
            entidade.TipoPlanilha = TipoPlanilhaEnum.Corretora;
            entidade.PercentualComissao = float.Parse(linha[INDEX_PERCENTUAL_COMISSAO].Replace(".", ","));
            entidade.Contrato = int.Parse(linha[INDEX_CONTRATO].Replace(".", "").Replace(" ", "").Replace("-", ""));

            return entidade;
        }

        public ContratoRelatorioErroDto LeEntidadeComErro(string[] linha)
        {
            ContratoRelatorioErroDto entidade = new ContratoRelatorioErroDto();

            entidade.DataComissao = linha[INDEX_DATA_COMISSAO];
            entidade.PercentualComissao = linha[INDEX_PERCENTUAL_COMISSAO];
            entidade.ValorCalculo = linha[INDEX_VALOR_COMISSAO];
            entidade.ValorEmprestimo = linha[INDEX_VALOR_EMPRESTIMO];
            entidade.Contrato = linha[INDEX_CONTRATO];
            entidade.Tabela = linha[INDEX_TABELA];
            entidade.Banco = "SAFRA";
            entidade.Produto = linha[INDEX_PRODUTO];

            return entidade;
        }
    }
}