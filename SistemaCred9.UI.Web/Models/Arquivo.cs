using System.Collections.Generic;
using System.Web;

namespace SistemaCred9.Web.UI.Models
{
    public class Arquivo
    {
        public const string CaminhoUpload = "~/Content/Uploads";

        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}