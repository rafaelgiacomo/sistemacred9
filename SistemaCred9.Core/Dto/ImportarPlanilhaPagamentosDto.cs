using SistemaCred9.Modelo;
using System.Collections.Generic;

namespace SistemaCred9.Core.Dto
{
    public class ImportarPlanilhaPagamentosDto
    {
        public List<ContratoRelatorioErroDto> ContratosErro { get; set; }
        public List<ContratoRelatorioPagamento> Contratos { get; set; }

        public ImportarPlanilhaPagamentosDto()
        {
            ContratosErro = new List<ContratoRelatorioErroDto>();
            Contratos = new List<ContratoRelatorioPagamento>();
        }
    }
}
