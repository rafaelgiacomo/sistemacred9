using System;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.ViewModels.Venda
{
    public class VendaStatusHistoricoViewModel
    {
        public int Id { get; set; }

        public int VendaId { get; set; }

        public DateTime Data { get; set; }

        public int StatusId { get; set; }

        public string Observacao { get; set; }

        public int UsuarioId { get; set; }

        public SelectList ListaStatus { get; set; }
    }
}