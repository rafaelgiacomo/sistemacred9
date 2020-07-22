using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Infra;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCred9.RoboImportacaoPagamentos
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger("LOGS\\Log - " + DateTime.Now.ToString() + ".txt");

            try
            {
                logger.EscreveLinha("==================================================================");
                logger.EscreveLinha("INÍCIO DE EXECUÇÃO");
                logger.EscreveLinha("==================================================================");
                logger.PulaLinhas(1);

                var arquivo = SelecionarArquivoImportacao();

                logger.EscreveLinha("=== ABRINDO CONEXÃO COM BANCO DE DADOS...");

                var unitOfWork = new UnitOfWork(new Cred9DbContext());
                var contratoNegocio = new ContratoRelatorioNegocio(unitOfWork);

                logger.EscreveLinha("OK - CONEXÃO ESTABELECIDA");
                logger.PulaLinhas(1);

                logger.EscreveLinha("=== INICIANDO IMPORTACAO...");
                logger.PulaLinhas(1);

                var response = contratoNegocio.RealizarImportacao(TipoPlanilhaEnum.Outros,
                                                                                   arquivo,
                                                                                   arquivo.Replace(Directory.GetCurrentDirectory() + "\\PlanilhasPagamentos\\", ""));

                if (response.Success)
                {

                    logger.EscreveLinha("REGISTROS IMPORTADOS COM SUCESSO");
                }
                else
                {
                    logger.EscreveLinha("OCORREU UM ERRO AO PROCESSAR! VERIFIQUE O FORMATO DA PLANILHA E SEUS REGISTROS: ");
                    logger.PulaLinhas(2);

                    foreach (var item in response.Data)
                    {
                        logger.EscreveLinha(string.Format("Contrato {0}, Data Lancamento {1} Percentual {2}", item.Contrato, item.DataLancamento, item.PercentualComissao));
                    }
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

            Console.ReadKey();
        }

        private static string SelecionarArquivoImportacao()
        {
            string nomeArquivo = string.Empty;
            bool nomeArquivook = false;

            while (!nomeArquivook)
            {
                try
                {
                    Console.WriteLine("=== LENDO ARQUIVOS DISPONIVEIS...");
                    Console.WriteLine("");

                    string[] arquivos = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\PlanilhasPagamentos");

                    int cont = 0;
                    foreach (var arq in arquivos)
                    {
                        cont++;
                        Console.WriteLine(cont + " - " + arq.Replace(Directory.GetCurrentDirectory() + "\\PlanilhasPagamentos\\", ""));
                    }

                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("DIGITE O Nº DO ARQUIVO PARA IMPORTAR: ");

                    var line = Console.ReadLine().ToString();
                    var cod = int.Parse(line);

                    if (cod <= arquivos.Length && cod > 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Arquivo escolhido: " + arquivos[cod - 1]);

                        Console.WriteLine("");
                        Console.WriteLine("Confirmar Execução ? (s/n)");
                        var resp = Console.ReadLine().ToString();

                        if ("S".Equals(resp) || "s".Equals(resp))
                        {
                            nomeArquivook = true;
                            nomeArquivo = arquivos[cod - 1];
                        }

                    }
                    else
                    {
                        Console.WriteLine("Arquivo não encontrado. Tente novamente!");
                        Console.WriteLine("");
                        Console.WriteLine("Aperte uma tecla para continuar");
                        Console.ReadKey();
                    }
                }
                catch
                {
                    Console.WriteLine("Digite um código válido!");
                    Console.WriteLine("");
                    Console.WriteLine("Aperte uma tecla para continuar");
                    Console.ReadKey();
                }

                Console.Clear();
            }

            return nomeArquivo;
        }
    }
}
