using System;
using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class Venda : EntitadeBase
    {
        public int ClienteId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime Data { get; set; }

        public int StatusId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<VendaStatusHistorico> Historico { get; set; }
    }
}
