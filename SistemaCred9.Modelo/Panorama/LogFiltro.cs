namespace SistemaCred9.Modelo.Panorama
{
    public class LogFiltro
    {
        public int ClienteId { get; set; }
        public int AgrupamentoId { get; set; }
        public Emprestimo Emprestimo { get; set; }
        public double Liquido { get; set; }
        public double Bruto { get; set; }
        public FiltroBancoPanorama FiltroBanco { get; set; }
        public string Observacao { get; set; }
        public bool Filtrado { get; set; }
    }
}
