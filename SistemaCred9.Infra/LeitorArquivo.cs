using SistemaCred9.Infra.Interface;
using System;
using System.IO;

namespace SistemaCred9.Infra
{
    public class LeitorArquivo : ILeitorArquivo, IDisposable
    {
        public StreamReader reader { get; set; }

        public LeitorArquivo(string caminho)
        {
            reader = new StreamReader(caminho);
        }

        public string[] LeLinha()
        {
            string linha = null;
            if ((linha = reader.ReadLine()) != null)
            {
                string[] colunas = linha.Split(';');
                return colunas;
            }
            return null;
        }

        public void FecharLeitor()
        {
            reader.Close();
        }

        public void Dispose()
        {
            FecharLeitor();
        }
    }
}
