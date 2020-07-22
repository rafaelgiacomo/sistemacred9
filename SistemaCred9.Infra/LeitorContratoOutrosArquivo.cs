using SistemaCred9.Core.Dto;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo;
using System;
using System.Collections.Generic;

namespace SistemaCred9.Infra
{
    public class LeitorContratoOutrosArquivo : ILeitorArquivoContratoPagamento
    {
        private readonly ILeitorArquivo _leitorArquivo;
        public const int INDEX_NOME_CLIENTE = 1;
        public const int INDEX_CPF = 0;
        public const int INDEX_DATA_COMISSAO = 6;
        public const int INDEX_TABELA = 4;
        public const int INDEX_PRODUTO = 5;
        public const int INDEX_BANCO = 3;
        public const int INDEX_PERCENTUAL_COMISSAO = 8;
        public const int INDEX_CONTRATO = 2;
        public const int INDEX_VALOR_COMISSAO = 7;
        public const int INDEX_VALOR_EMPRESTIMO = 9;
        public const string CHARACTER_NULO = "";

        private bool _ehFimArquivo;
        private string _nomeArquivo;

        #region Propriedades

        public bool EhFimArquivo { get { return _ehFimArquivo; } }
        public List<ContratoRelatorioPagamento> Contratos { get; set; }
        public List<ContratoRelatorioErroDto> LinhasComErro { get; set; }

        #endregion

        public LeitorContratoOutrosArquivo(ILeitorArquivo leitorArquivo, string nomeArquivo)
        {
            _leitorArquivo = leitorArquivo;
            _nomeArquivo = nomeArquivo;
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

            var dataComissao = linha[INDEX_DATA_COMISSAO];
            var valorComissao = linha[INDEX_VALOR_COMISSAO];

            //entidade.ValorEmprestimo = float.Parse(linha[INDEX_VALOR_EMPRESTIMO].Replace("R$", "").Replace(" ", ""));
            entidade.NomeCliente = linha[INDEX_NOME_CLIENTE];
            entidade.DataImportacao = DateTime.Now;
            entidade.Cpf = linha[INDEX_CPF];
            entidade.Tabela = linha[INDEX_TABELA];
            entidade.Banco = linha[INDEX_BANCO];
            entidade.Produto = linha[INDEX_PRODUTO];
            entidade.NomeArquivo = _nomeArquivo;
            entidade.TipoPlanilha = TipoPlanilhaEnum.Outros;
            entidade.PercentualComissao = float.Parse(linha[INDEX_PERCENTUAL_COMISSAO].Replace(".", ","));
            entidade.Contrato = int.Parse(linha[INDEX_CONTRATO].Replace(".", "").Replace(" ", "").Replace("-", ""));

            if (string.IsNullOrEmpty(dataComissao))
            {
                entidade.DataComissao = DateTime.Parse(linha[INDEX_DATA_COMISSAO]);
            }

            if (string.IsNullOrEmpty(valorComissao))
            {
                entidade.ValorComissao = float.Parse(linha[INDEX_VALOR_COMISSAO].Replace("R$", "").Replace(" ", ""));
            }

            if (entidade.ValorComissao < 0)
            {
                entidade.PercentualComissao *= (-1);
            }

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
            entidade.NomeCliente = linha[INDEX_NOME_CLIENTE];
            entidade.Cpf = linha[INDEX_CPF];
            entidade.Tabela = linha[INDEX_TABELA];
            entidade.Banco = linha[INDEX_BANCO];
            entidade.Produto = linha[INDEX_PRODUTO];

            return entidade;
        }
    }
}