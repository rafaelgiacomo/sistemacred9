using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCred9.Modelo
{
    public class Venda
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int CpfCliente { get; set; }

        public DateTime Data { get; set; }

        public VendaStatusEnum Status { get; set; }

        public List<VendaStatusHistorico> Historico { get; set; }
    }
}
