namespace SistemaCred9.Infra.Interface
{
    public interface ILogger
    {
        void EscreveLinha(string linha);
        void Escreve(string conteudo);
        void PulaLinhas(int qtd);
        void Fechar();
    }
}
