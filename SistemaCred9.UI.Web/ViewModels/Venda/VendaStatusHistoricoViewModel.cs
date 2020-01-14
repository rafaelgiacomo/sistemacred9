using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.ViewModels.Venda
{
    public class VendaStatusHistoricoViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public int VendaId { get; set; }

        public DateTime Data { get; set; }

        public int StatusId { get; set; }

        public string Observacao { get; set; }

        public int UsuarioId { get; set; }

        [Display(Name = "Status")]
        public string StatusDescricao { get { return ((VendaStatusEnum)StatusId).ToString(); } }

        public SelectList ListaStatus { get; set; }

        public SistemaCred9.Modelo.Usuario Usuario { get; set; }

        public VendaViewModel Venda { get; set; }

        public VendaStatusHistoricoViewModel() : base()
        {

        }
    }
}