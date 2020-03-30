using System.Collections.Generic;

namespace SistemaCred9.Modelo.Panorama
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public bool Ativado { get; set; }
        public string TelefoneContato { get; set; }
        public string CelularContato { get; set; }
        public StatusTelemarketing Status { get; set; }
        public List<Beneficio> Beneficios { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }
        public List<Telefone> Telefones { get; set; }

        public Cliente()
        {
            Emprestimos = new List<Emprestimo>();
            Telefones = new List<Telefone>();
            Beneficios = new List<Beneficio>();
            Ativado = true;
        }
    }
}
