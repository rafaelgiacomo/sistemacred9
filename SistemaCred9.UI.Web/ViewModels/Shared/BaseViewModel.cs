using System.Collections.Generic;
using System.Linq;
using SistemaCred9.Modelo.Panorama;

namespace SistemaCred9.Web.UI.ViewModels.Shared
{
    public class BaseViewModel
    {
        public int[] ArrayQtdPorStatus { get; set; }

        public int StatusIdAtual { get; set; }

        public string MenuAtual { get; set; }

        public List<StatusTarefa> ListaStatusTarefa { get; set; }


        public BaseViewModel()
        {
            ArrayQtdPorStatus = new int[15];
            ListaStatusTarefa = new List<StatusTarefa>();
        }

        public string[] ArrayNomesStatusTarefa()
        {
            return ListaStatusTarefa.Select(x => x.Descricao).ToArray();
        }

        public string[] ArrayActionStatusTarefa()
        {
            var lista = new List<string>();

            foreach (var item in ListaStatusTarefa)
            {
                lista.Add("Index");
            }

            return lista.ToArray();
        }

        public string[] ArrayControllerStatusTarefa()
        {
            var lista = new List<string>();

            foreach (var item in ListaStatusTarefa)
            {
                lista.Add("Tarefa");
            }

            return lista.ToArray();
        }

        public object[] ArrayParametrosStatusTarefa()
        {
            var lista = new List<object>();

            foreach (var item in ListaStatusTarefa)
            {
                lista.Add(new { statusTarefaId = item.Id });
            }

            return lista.ToArray();
        }

    }
}