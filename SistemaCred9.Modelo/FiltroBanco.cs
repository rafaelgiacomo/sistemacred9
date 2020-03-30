using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class FiltroBanco : EntitadeBase
    {
        public FiltroBanco()
        {
            ListaFiltro = new List<Filtro>();
        }

        public string Banco { get; set; }
        public float MinLiquido { get; set; }
        public int MinParcela { get; set; }
        public int Prazo { get; set; }
        public int MinParcelasEmAberto { get; set; }
        public int MaxParcelasEmAberto { get; set; }
        public float Coeficiente { get; set; }

        public virtual ICollection<Filtro> ListaFiltro { get; set; }
    }
}
