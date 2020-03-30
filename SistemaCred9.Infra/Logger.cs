using SistemaCred9.Infra.Interface;
using System;
using System.IO;

namespace SistemaCred9.Infra
{
    public class Logger : ILogger
    {
        private readonly StreamWriter escritor;

        public Logger(string arquivo)
        {
            arquivo = arquivo.Replace("/", "_");
            arquivo = arquivo.Replace(":", "_");
            escritor = new StreamWriter(arquivo);
        }

        public void EscreveLinha(string linha)
        {
            Console.WriteLine(linha);
            escritor.WriteLine(linha);
        }

        public void Escreve(string conteudo)
        {
            Console.Write(conteudo);
            escritor.Write(conteudo);
        }

        public void PulaLinhas(int qtd)
        {
            for (int i = 0; i < qtd; i++)
            {
                Console.WriteLine(string.Empty);
                escritor.WriteLine(string.Empty);
            }
        }

        public void Fechar()
        {
            escritor.Flush();
            escritor.Close();
        }
    }
}
