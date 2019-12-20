using System;

namespace SistemaCred9.Modelo
{
    public class VendaStatusHistorico : EntitadeBase
    {
        public int VendaId { get; set; }

        public DateTime Data { get; set; }

        public int StatusId { get; set; }

        public string Observacao { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Venda Venda { get; set; }
    }
}
