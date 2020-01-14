using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Shared;
using SistemaCred9.Web.UI.ViewModels.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.ViewModels.Venda
{
    public class VendaViewModel : BaseViewModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Código do cliente Panorama")]
        public int ClienteId { get; set; }

        [Display(Name = "Cpf do Cliente")]
        public int CpfCliente { get; set; }

        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public DateTime Data { get; set; }

        public int StatusId { get; set; }

        [Display(Name = "Status")]
        public string StatusDescricao { get { return ((VendaStatusEnum)StatusId).ToString(); } }

        public UsuarioViewModel Usuario { get; set; }

        public List<VendaStatusHistoricoViewModel> Historico { get; set; }

        public VendaViewModel() : base()
        {

        }
    }
}