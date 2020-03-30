using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;

namespace SistemaCred9.Infra
{
    public class GeradorCsvDiscador
    {
        private readonly IEscritorArquivo _escritor;
        private const string SEPARADOR = ";";

        public GeradorCsvDiscador(IEscritorArquivo escritor)
        {
            _escritor = escritor;
        }

        public void EscreveCabecalho()
        {
            _escritor.Escreve("nome_cliente");
            _escritor.Escreve(SEPARADOR);
            _escritor.Escreve("cpf");
            _escritor.Escreve(SEPARADOR);
            _escritor.Escreve("cod_link_int");
            _escritor.Escreve(SEPARADOR);
            _escritor.Escreve("cod_link_char");
            _escritor.Escreve(SEPARADOR);

            for (int i = 1; i <= 10; i++)
            {
                _escritor.Escreve("fone" + i);
                _escritor.Escreve(SEPARADOR);
            }

            _escritor.Escreve("banco");
            _escritor.EscreveLinha(SEPARADOR);
        }

        public void EscreveCliente(Cliente cliente)
        {
            _escritor.Escreve(cliente.Nome);
            _escritor.Escreve(SEPARADOR);
            _escritor.Escreve(cliente.Cpf);
            _escritor.Escreve(SEPARADOR);
            _escritor.Escreve(string.Empty);
            _escritor.Escreve(SEPARADOR);
            _escritor.Escreve(string.Empty);
            _escritor.Escreve(SEPARADOR);
            _escritor.Escreve(cliente.TelefoneContato);
            _escritor.Escreve(SEPARADOR);
            _escritor.Escreve(string.Empty);
            _escritor.Escreve(cliente.CelularContato);

            for (int i = 1; i <= 8; i++)
            {
                if (cliente.Telefones.Count > 0 && cliente.Telefones.Count >= i)
                {
                    _escritor.Escreve(cliente.Telefones[i - 1].Ddd);
                    _escritor.Escreve(cliente.Telefones[i - 1].Numero);
                    _escritor.Escreve(SEPARADOR);
                }
            }

            _escritor.Escreve(string.Empty);
            _escritor.EscreveLinha(SEPARADOR);
        }

        public void EncerraCsv()
        {
            _escritor.FecharArquivo();
        }

    }
}
