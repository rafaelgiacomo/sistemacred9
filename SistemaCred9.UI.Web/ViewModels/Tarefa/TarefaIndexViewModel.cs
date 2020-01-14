using SistemaCred9.Modelo.Panorama;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;

namespace SistemaCred9.Web.UI.ViewModels.Tarefa
{
    public class TarefaIndexViewModel : BaseViewModel
    {
        public string StatusAtual { get; set; }

        public List<Contrato> Contratos { get; set; }

        public int OpcaoProprietarioSelecionado { get; set; }

        public TarefaIndexViewModel()
        {
            Contratos = new List<Contrato>();
        }
    }
}