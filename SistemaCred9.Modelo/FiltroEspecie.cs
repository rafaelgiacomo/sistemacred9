using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class FiltroEspecie : EntitadeBase
    {
        public FiltroEspecie()
        {
            ListaFiltro = new List<Filtro>();
        }

        public string Descricao { get; set; }
        public int CodEspecie { get; set; }

        public virtual ICollection<Filtro> ListaFiltro { get; set; }
    }
}
