using SistemaCred9.Infra.Interface;
using System;

namespace SistemaCred9.Infra
{
    public class LoggerConsole : ILogger
    {
        public void Escreve(string conteudo)
        {
            Console.Write(conteudo);
        }

        public void EscreveLinha(string linha)
        {
            Console.WriteLine(linha);
        }

        public void Fechar()
        {
            throw new NotImplementedException();
        }

        public void PulaLinhas(int qtd)
        {
            for (int i = 0; i < qtd; i++)
            {
                Console.WriteLine(string.Empty);
            }
        }
    }
}
