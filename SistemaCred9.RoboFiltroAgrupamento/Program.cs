using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Infra;
using SistemaCred9.Modelo;
using SistemaCred9.Modelo.Panorama;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using System;
using System.Configuration;
using System.Linq;

namespace SistemaCred9.RoboFiltroAgrupamento
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbPanorama"].ConnectionString;
            const string nomeRobo = "FILTRO_AGRUPAMENTO";
            var logger = new Logger("LOGS\\Log - " + DateTime.Now.ToString() + ".txt");

            try
            {
                var filtroSelecionado = SelecionaFiltroPeloUsuario();

                logger.EscreveLinha("==================================================================");
                logger.EscreveLinha("INÍCIO DE EXECUÇÃO");
                logger.EscreveLinha("==================================================================");
                logger.PulaLinhas(1);
                logger.EscreveLinha("=== ABRINDO CONEXÃO COM BANCO DE DADOS...");

                ClienteNegocio cliBus = new ClienteNegocio(connectionString, logger);
                AgrupamentoNegocio agpBus = new AgrupamentoNegocio(connectionString, logger);

                logger.EscreveLinha("OK - CONEXÃO ESTABELECIDA");
                logger.PulaLinhas(1);

                logger.EscreveLinha("=== LENDO PARAMETROS DE FILTRO...");
                var parametros = RecebeParametros(filtroSelecionado);
                logger.EscreveLinha("OK - PARAMETROS RECEBIDOS");
                logger.PulaLinhas(1);

                logger.EscreveLinha("=== RECUPERANDO CLIENTES DO AGRUPAMENTO...");
                var clientes = cliBus.ListarClientesDeAgrupamento(parametros.Filtro.CodAgrupamento);
                logger.EscreveLinha("OK - TOTAL DE CLIENTES DO AGRUPAMENTO '" + parametros.Filtro.CodAgrupamento + "': " + clientes.Count);
                logger.PulaLinhas(1);

                if (clientes.Count > 0)
                {
                    var data = DateTime.Now.ToString();

                    var agp = new Agrupamento()
                    {
                        Descricao = nomeRobo + " (Dentro do perfil) " + "" + data,
                        Cor = "cde9e0"
                    };

                    var agpFora = new Agrupamento()
                    {
                        Descricao = nomeRobo + " (Fora do perfil) " + "" + data,
                        Cor = "cde9e0"
                    };

                    var agpTelefone = new Agrupamento()
                    {
                        Descricao = nomeRobo + " (Telefones Desatualizados) " + "" + data,
                        Cor = "cde9e0"
                    };

                    logger.PulaLinhas(1);
                    logger.EscreveLinha("=== RECUPERANDO BENEFICIOS DE CADA CLIENTE...");
                    cliBus.ConsultaBeneficiosClientes(clientes);
                    logger.EscreveLinha("OK - BENEFICIOS DE CLIENTES RECUPERADO");
                    logger.PulaLinhas(1);

                    logger.EscreveLinha("=== RECUPERANDO EMPRESTIMOS DE CADA CLIENTE...");
                    cliBus.ConsultaEmprestimosClientes(clientes);
                    logger.EscreveLinha("OK - EMPRESTIMOS DE CLIENTES RECUPERADO");
                    logger.PulaLinhas(1);

                    logger.EscreveLinha("=== FAZENDO CALCULOS DE CADA CLIENTE...");
                    cliBus.LimparClientesForaDoPerfil(clientes, parametros.Filtro);
                    logger.EscreveLinha("OK - TOTAL DE CLIENTES DENTRO DO PERFIL COM CALCULOS REALIZADOS: " + clientes.Count);
                    logger.PulaLinhas(1);

                    logger.EscreveLinha("=== CRIANDO AGRUPAMENTOS...");
                    agpBus.AdicionarAgrupamento(agp);
                    logger.EscreveLinha("OK - AGRUPAMENTO CRIADO: " + agp.Descricao + " - Código " + agp.Id);
                    agpBus.AdicionarAgrupamento(agpFora);
                    logger.EscreveLinha("OK - AGRUPAMENTO CRIADO: " + agpFora.Descricao + " - Código " + agpFora.Id);
                    agpBus.AdicionarAgrupamento(agpTelefone);
                    logger.EscreveLinha("OK - AGRUPAMENTO CRIADO: " + agpTelefone.Descricao + " - Código " + agpTelefone.Id);
                    logger.PulaLinhas(1);

                    logger.EscreveLinha("ADICIONANDO CLIENTES NOS AGRUPAMENTOS...");
                    logger.EscreveLinha("==================================================================");
                    int qtdDentroPerfil = clientes.Where(x => x.Ativado == true).Count();
                    int qtdForaPerfil = clientes.Where(x => x.Ativado == false).Count();

                    if (agp.Id > 0)
                    {
                        int porcentAnterior = 0;
                        for (int i = 0; i < clientes.Count; i++)
                        {
                            var c = clientes[i];

                            float porcentagem = i / (float)clientes.Count;
                            porcentagem = (porcentagem * 100);

                            if ((int)porcentagem > porcentAnterior)
                            {
                                porcentAnterior = (int)porcentagem;
                                logger.EscreveLinha(porcentAnterior + " % dos clientes adicionados de " + clientes.Count);
                            }

                            if (c.Ativado)
                            {
                                if (!agpBus.ConsultaClientePresente(c.Id, parametros.Filtro.AgrupamentosRestricao[0]))
                                {
                                    //Cliente valido que precisa atualizar telfone
                                    agpBus.AdicionarClienteNoAgrupamento(agpTelefone.Id, c.Id);
                                }
                                else
                                {
                                    //Cliente valido que não precisa atualizar telefone
                                    agpBus.AdicionarClienteNoAgrupamento(agp.Id, c.Id);
                                }
                            }
                            else
                            {
                                //Cliente fora do perfil do filtro
                                agpBus.AdicionarClienteNoAgrupamento(agpFora.Id, c.Id);
                            }
                        }
                    }

                    logger.EscreveLinha("==================================================================");
                    logger.EscreveLinha("OK - " + qtdDentroPerfil + " CLIENTES DENTRO DO PERFIL");
                    logger.EscreveLinha("OK - " + qtdForaPerfil + " CLIENTES FORA DO PERFIL");
                }
            }
            catch (Exception ex)
            {
                logger.EscreveLinha("OCORREU UM ERRO AO PROCESSAR...");
                logger.PulaLinhas(2);
                logger.EscreveLinha("MENSAGEM DE ERRO: ");
                logger.EscreveLinha(ex.Message);
            }

            logger.PulaLinhas(1);
            logger.EscreveLinha("==================================================================");
            logger.EscreveLinha("PROCESSAMENTO FINALIZADO");
            logger.EscreveLinha("==================================================================");
            logger.PulaLinhas(1);
            logger.Fechar();
        }

        private static Filtro SelecionaFiltroPeloUsuario()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
            var filtroNegocio = new FiltroNegocio(unitOfWork);
            Filtro filtroSelecionado = null;
            bool codigoFiltroOk = false;

            while (!codigoFiltroOk)
            {
                try
                {
                    var listaFiltros = filtroNegocio.ListarTodos();
                    Console.WriteLine("Filtros Cadastrados");
                    Console.WriteLine("==================================================================");

                    foreach (var item in listaFiltros)
                    {
                        Console.WriteLine(item.Id + " - " + item.Descricao);
                    }
                    
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("DIGITE O CODIGO DO FILTRO: ");

                    var line = Console.ReadLine().ToString();
                    var cod = int.Parse(line);

                    filtroSelecionado = filtroNegocio.SelecionarPorId(cod);

                    if (filtroSelecionado != null)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Filtro: " + filtroSelecionado.Descricao);

                        Console.WriteLine("");
                        Console.WriteLine("Confirmar Execução ? (s/n)");
                        var resp = Console.ReadLine().ToString();

                        if ("S".Equals(resp) || "s".Equals(resp))
                        {
                            codigoFiltroOk = true;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Filtro não encontrado. Tente novamente!");
                        Console.WriteLine("");
                        Console.WriteLine("Aperte uma tecla para continuar");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Digite um código válido!");
                    Console.WriteLine("");
                    Console.WriteLine("Aperte uma tecla para continuar");
                    Console.ReadKey();
                }

                Console.Clear();
            }

            return filtroSelecionado;
        }

        private static ParametrosRobo RecebeParametros(Filtro filtroSelecionado)
        {
            ParametrosRobo param = new ParametrosRobo(filtroSelecionado);

            #pragma warning disable CS0618 // O tipo ou membro é obsoleto

            param.LeAgrupamentosDeRestricao(ConfigurationSettings.AppSettings["paramsAgrupamentos"].ToString());

            #pragma warning restore CS0618 // O tipo ou membro é obsoleto

            return param;
        }
    }
}
