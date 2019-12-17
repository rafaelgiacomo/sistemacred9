using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCred9.Modelo
{
    public class VendaStatusHistorico
    {
        public int Id { get; set; }

        public int VendaId { get; set; }

        public DateTime Data { get; set; }

        public VendaStatusEnum Status { get; set; }

        public string Observacao { get; set; }

        public int UsuarioId { get; set; }

        public Venda Venda { get; set; }
    }
}
