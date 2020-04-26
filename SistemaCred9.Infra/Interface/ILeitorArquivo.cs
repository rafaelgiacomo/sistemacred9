namespace SistemaCred9.Infra.Interface
{
    public interface ILeitorArquivo
    {
        string[] LeLinha();

        void FecharLeitor();
    }
}
