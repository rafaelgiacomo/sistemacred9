namespace SistemaCred9.Modelo.Panorama
{
    public class FiltroBancoPanorama
    {
        public string Banco { get; set; }
        public int Prazo { get; set; }
        public int MinParcelasEmAberto { get; set; }
        public int MaxParcelasEmAberto { get; set; }
        public float MinParcela { get; set; }
        public float MinLiquido { get; set; }
        public double Coeficiente { get; set; }
    }
}
