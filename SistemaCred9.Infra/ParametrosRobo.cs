using SistemaCred9.Modelo;
using SistemaCred9.Modelo.Panorama;
using System.Xml.Linq;

namespace SistemaCred9.Infra
{
    public class ParametrosRobo
    {
        public FiltroClientePanorama Filtro { get; set; }

        public ParametrosRobo()
        {
            Filtro = new FiltroClientePanorama();
        }

        public ParametrosRobo(Filtro filtroSelecionado)
        {
            Filtro = new FiltroClientePanorama();

            Filtro.CodAgrupamento = filtroSelecionado.CodAgrupamento;
            Filtro.LimiteRegistros = (int)filtroSelecionado.LimiteRegistros;
            Filtro.MinDataNascimento = filtroSelecionado.MinDataNascimento;

            foreach (var b in filtroSelecionado.ListaFiltroBanco)
            {
                var filtro = new FiltroBancoPanorama();

                filtro.Banco = b.Banco;
                filtro.MinLiquido = b.MinLiquido;
                filtro.MinParcela = b.MinParcela;
                filtro.Prazo = b.Prazo;
                filtro.MinParcelasEmAberto = b.MinParcelasEmAberto;
                filtro.MaxParcelasEmAberto = b.MaxParcelasEmAberto;
                filtro.Coeficiente = b.Coeficiente;

                Filtro.FiltroBanco.Add(filtro);
            }

            foreach (var e in filtroSelecionado.ListaFiltroEspecie)
            {
                Filtro.BeneficiosInvalidos.Add(e.CodEspecie.ToString());
            }
        }

        public void LeParametrosFixos(string arquivo)
        {
            XElement xml = XElement.Load(arquivo);

            foreach (XElement x in xml.Elements())
            {
                Filtro.CodAgrupamento = int.Parse(x.Attribute("codAgrupamento").Value);
                Filtro.LimiteRegistros = int.Parse(x.Attribute("limiteRegistros").Value);
                Filtro.MinDataNascimento = x.Attribute("minDataNascimento").Value;
            }
        }

        public void LeAgrupamentosDeRestricao(string arquivo)
        {
            XElement xml = XElement.Load(arquivo);

            foreach (XElement x in xml.Elements())
            {
                Filtro.AgrupamentosRestricao.Add(int.Parse(x.Attribute("codAgrupamento").Value));
            }
        }

        public void LeParametrosBancos(string arquivo)
        {
            XElement xml = XElement.Load(arquivo);

            foreach (XElement x in xml.Elements())
            {
                var filtro = new FiltroBancoPanorama();

                filtro.Banco = x.Attribute("banco").Value;
                filtro.MinLiquido = float.Parse(x.Attribute("minLiquido").Value);
                filtro.MinParcela = float.Parse(x.Attribute("minParcela").Value);
                filtro.Prazo = int.Parse(x.Attribute("prazo").Value);
                filtro.MinParcelasEmAberto = int.Parse(x.Attribute("minParcelasEmAberto").Value);
                filtro.MaxParcelasEmAberto = int.Parse(x.Attribute("maxParcelasEmAberto").Value);
                filtro.Coeficiente = double.Parse(x.Attribute("coeficiente").Value);

                Filtro.FiltroBanco.Add(filtro);
            }
        }

        public void LeParametrosEspecies(string arquivo)
        {
            XElement xml = XElement.Load(arquivo);

            foreach (XElement x in xml.Elements())
            {
                Filtro.BeneficiosInvalidos.Add(x.Attribute("codEspecie").Value);
            }
        }
    }
}
