using SistemaCred9.Infra.Interface;
using System.IO;

namespace SistemaCred9.Infra
{
    public class EscritorArquivo : IEscritorArquivo
    {
        private readonly StreamWriter escritor;

        public EscritorArquivo(string arquivo)
        {
            arquivo = arquivo.Replace("/", "_");
            arquivo = arquivo.Replace(":", "_");
            escritor = new StreamWriter(arquivo);
        }

        public void Escreve(string conteudo)
        {
            escritor.Write(conteudo);
        }

        public void EscreveLinha(string linha)
        {
            escritor.WriteLine(linha);
        }

        public void FecharArquivo()
        {
            escritor.Flush();
            escritor.Close();
        }

        public void PulaLinhas(int qtd)
        {
            for (int i = 0; i < qtd; i++)
            {
                escritor.WriteLine(string.Empty);
            }
        }
    }
}
