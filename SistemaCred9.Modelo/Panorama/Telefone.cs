namespace SistemaCred9.Modelo.Panorama
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
        public int ClienteId { get; set; }
        public int Status { get; set; }
        public string Data { get; set; }
        public string DataAcerto { get; set; }
        public string Origem { get; set; }
    }
}
