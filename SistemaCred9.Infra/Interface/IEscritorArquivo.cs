namespace SistemaCred9.Infra.Interface
{
    public interface IEscritorArquivo
    {
        void EscreveLinha(string linha);
        void Escreve(string conteudo);
        void PulaLinhas(int qtd);
        void FecharArquivo();
    }
}
