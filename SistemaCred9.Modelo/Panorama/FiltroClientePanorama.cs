using System.Collections.Generic;

namespace SistemaCred9.Modelo.Panorama
{
    public class FiltroClientePanorama
    {
        public int CodAgrupamento { get; set; }
        public string MinDataNascimento { get; set; }
        public int LimiteRegistros { get; set; }
        public List<int> AgrupamentosRestricao { get; set; }
        public List<string> BeneficiosInvalidos { get; set; }
        public List<FiltroBancoPanorama> FiltroBanco { get; set; }

        public FiltroClientePanorama()
        {
            AgrupamentosRestricao = new List<int>();
            BeneficiosInvalidos = new List<string>();
            FiltroBanco = new List<FiltroBancoPanorama>();
        }
    }
}