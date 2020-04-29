using SistemaCred9.Core.Dto;
using SistemaCred9.Modelo;
using System.Collections.Generic;

namespace SistemaCred9.Infra.Interface
{
    public interface ILeitorArquivoContratoPagamento
    {
        bool EhFimArquivo { get; }
        List<ContratoRelatorioPagamento> Contratos { get; set; }
        List<ContratoRelatorioErroDto> LinhasComErro { get; set; }

        void PulaCabecalho();

        void LeConfigProximaLinha();

        ContratoRelatorioPagamento LeEntidade(string[] linha);

        ContratoRelatorioErroDto LeEntidadeComErro(string[] linha);
    }
}
