using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class Filtro : EntitadeBase
    {
        public Filtro()
        {
            ListaFiltroEspecie = new List<FiltroEspecie>();
            ListaFiltroBanco = new List<FiltroBanco>();
        }

        public bool Ativo { get; set; }
        public string Descricao { get; set; }
        public int CodAgrupamento { get; set; }
        public long LimiteRegistros { get; set; }
        public string MinDataNascimento { get; set; }

        public virtual ICollection<FiltroEspecie> ListaFiltroEspecie { get; set; }
        public virtual ICollection<FiltroBanco> ListaFiltroBanco { get; set; }
    }
}
