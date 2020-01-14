using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaCred9.Modelo
{
    public class VendaStatusHistorico : EntitadeBase
    {
        [Display(Name = "Nome do Diretor")]
        public int VendaId { get; set; }

        public DateTime Data { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Venda Venda { get; set; }
    }
}
