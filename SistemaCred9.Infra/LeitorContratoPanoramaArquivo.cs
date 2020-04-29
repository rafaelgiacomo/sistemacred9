using SistemaCred9.Core.Dto;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo;
using System;
using System.Collections.Generic;

namespace SistemaCred9.Infra
{
    public class LeitorContratoPanoramaArquivo
    {
        private readonly ILeitorArquivo _leitorArquivo;
        public const int INDEX_NOME_CLIENTE = 0;
        public const int INDEX_CPF = 1;
        public const int INDEX_DATA_LANCAMENTO = 2;
        public const int INDEX_ASSESSOR = 5;
        public const int INDEX_TABELA = 8;
        public const int INDEX_BANCO = 11;
        public const int INDEX_PERCENTUAL_COMISSAO = 17;
        public const int INDEX_CONTRATO = 26;
        public const string CHARACTER_NULO = "";

        private bool _ehFimArquivo;

        #region Propriedades

        public bool EhFimArquivo { get { return _ehFimArquivo; } }
        public List<ContratoRelatorio> Contratos { get; set; }
        public List<ContratoRelatorioErroDto> LinhasComErro { get; set; }

        #endregion

        public LeitorContratoPanoramaArquivo(ILeitorArquivo leitorArquivo)
        {
            _leitorArquivo = leitorArquivo;
            Contratos = new List<ContratoRelatorio>();
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

        public ContratoRelatorio LeEntidade(string[] linha)
        {
            ContratoRelatorio entidade = new ContratoRelatorio();

            entidade.NomeCliente = linha[INDEX_NOME_CLIENTE];
            entidade.Cpf = linha[INDEX_CPF];
            entidade.NomeAssessor = linha[INDEX_ASSESSOR];
            entidade.Tabela = linha[INDEX_TABELA];
            entidade.Banco = linha[INDEX_BANCO];
            entidade.DataLancamento = DateTime.Parse(linha[INDEX_DATA_LANCAMENTO]);
            entidade.PercentualComissao = float.Parse(linha[INDEX_PERCENTUAL_COMISSAO].Replace(".", ","));
            entidade.Contrato = int.Parse(linha[INDEX_CONTRATO].Replace(".", "").Replace(" ", "").Replace("-", ""));

            return entidade;
        }

        public ContratoRelatorioErroDto LeEntidadeComErro(string[] linha)
        {
            ContratoRelatorioErroDto entidade = new ContratoRelatorioErroDto();

            entidade.DataLancamento = linha[INDEX_DATA_LANCAMENTO];
            entidade.PercentualComissao = linha[INDEX_PERCENTUAL_COMISSAO];
            entidade.Contrato = linha[INDEX_CONTRATO];
            entidade.NomeCliente = linha[INDEX_NOME_CLIENTE];
            entidade.Cpf = linha[INDEX_CPF];
            entidade.NomeAssessor = linha[INDEX_ASSESSOR];
            entidade.Tabela = linha[INDEX_TABELA];
            entidade.Banco = linha[INDEX_BANCO];

            return entidade;
        }
    }
}