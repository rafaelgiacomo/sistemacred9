using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Usuario;
using System;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Venda
{
    public class VendaViewModel
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int CpfCliente { get; set; }

        public int UsuarioId { get; set; }

        public string Observacao { get; set; }

        public UsuarioViewModel Usuario { get; set; }

        public DateTime Data { get; set; }

        public VendaStatusEnum Status { get; set; }

        public ICollection<VendaStatusHistorico> Historico { get; set; }
    }
}