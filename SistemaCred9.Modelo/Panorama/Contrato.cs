using System;

namespace SistemaCred9.Modelo.Panorama
{
    public class Contrato : EntitadeBase
    {
        public int ClienteId { get; set; }

        public int NumContrato { get; set; }

        public int UsuarioId { get; set; }

        public int StatusTarefaExecucaoId { get; set; }

        public DateTime DataLancamento { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
