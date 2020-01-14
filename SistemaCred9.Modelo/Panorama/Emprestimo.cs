namespace SistemaCred9.Modelo.Panorama
{
    public class Emprestimo
    {
        public int Id { get; set; }

        public int BeneficioId { get; set; }

        public string Banco { get; set; }

        public int Prazo { get; set; }

        public int ParcelasEmAberto { get; set; }

        public double ValorParcela { get; set; }

        public double Saldo { get; set; }
    }
}
