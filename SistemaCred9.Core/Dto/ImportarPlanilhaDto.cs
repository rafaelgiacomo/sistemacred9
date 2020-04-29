using SistemaCred9.Modelo;
using System.Collections.Generic;

namespace SistemaCred9.Core.Dto
{
    public class ImportarPlanilhaDto
    {
        public List<ContratoRelatorioErroDto> ContratosErro { get; set; }
        public List<ContratoRelatorio> Contratos { get; set; }

        public ImportarPlanilhaDto()
        {
            ContratosErro = new List<ContratoRelatorioErroDto>();
            Contratos = new List<ContratoRelatorio>();
        }
    }
}
