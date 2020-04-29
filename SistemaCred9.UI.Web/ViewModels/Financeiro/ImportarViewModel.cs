using SistemaCred9.Modelo;
using SistemaCred9.Web.UI.ViewModels.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.ViewModels.Financeiro
{
    public class ImportarViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Selecione pelo menos um arquivo para upload")]
        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        [Display(Name = "Tipo de Planilha")]
        public SelectList TipoPlanilha { get; set; }

        [Required(ErrorMessage = "Selecione um tipo de planilha para importação")]
        public int TipoPlanilhaId { get; set; }

        public string TipoPLanilhaoDescricao { get { return ((TipoPlanilhaEnum)TipoPlanilhaId).ToString(); } }

        public ImportarViewModel()
        {
            TipoPlanilha = new SelectList(TipoPlanilhaModelo.ListarTodos(), "Id", "Descricao");
        }
    }
}