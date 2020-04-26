using System.Collections.Generic;

namespace SistemaCred9.Modelo
{
    public class TabelaComissao : EntitadeBase
    {
        public string Descricao { get; set; }
        public float ValorComissao { get; set; }

        public virtual List<ContratoRelatorio> Contratos { get; set; }
    }
}
