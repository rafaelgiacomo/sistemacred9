using SistemaCred9.Core.Dto;
using SistemaCred9.Core.Resposta;
using SistemaCred9.Infra;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaCred9.Negocio
{
    public class ContratoRelatorioNegocio
    {
        private readonly UnitOfWork _unitOfWork;

        public ContratoRelatorioNegocio(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DataResponse<ContratoRelatorio> Obter(int id)
        {
            var response = new DataResponse<ContratoRelatorio>();

            try
            {
                response.Data = _unitOfWork.ContratoRelatorio.Obter(id);
                response.Success = true;
            }
            catch
            {
                response.Success = false;
            }

            return response;
        }

        public BaseResponse ValidarContratos(List<int> ids)
        {
            var response = new DataResponse<ContratoRelatorio>();

            try
            {
                _unitOfWork.BeginTransaction();

                foreach (var id in ids)
                {
                    var entidade = _unitOfWork.ContratoRelatorio.Obter(id);
                    entidade.Exportado = true;

                    _unitOfWork.ContratoRelatorio.Atualizar(entidade);
                }

                response.Success = true;

                _unitOfWork.Salvar();
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                response.FailWithMessage("Não foi possível validar contratos");
            }

            return response;
        }

        public DataResponse<List<ContratoRelatorioErroDto>> RealizarImportacao(TipoPlanilhaEnum tipoPlanilha, string caminho, string nomeArquivo)
        {
            var respostaFinal = new DataResponse<List<ContratoRelatorioErroDto>>();

            if (tipoPlanilha == TipoPlanilhaEnum.Panorama)
            {
                var response = ListarContratosArquivoPanorama(caminho);

                if (response.Success)
                {
                    AdicionarListaContratoRelatorioPanorama(response.Data.Contratos);
                    respostaFinal.Success = true;
                }
                else
                {
                    respostaFinal.Data = response.Data.ContratosErro;
                    respostaFinal.Success = false;
                }
            }
            else
            {
                var response = ListarContratosArquivoPagamentos(tipoPlanilha, caminho, nomeArquivo);

                if (response.Success)
                {
                    AdicionarListaContratoRelatorioPagamento(response.Data.Contratos);
                    respostaFinal.Success = true;
                }
                else
                {
                    respostaFinal.Data = response.Data.ContratosErro;
                    respostaFinal.Success = false;
                }
            }

            return respostaFinal;
        }

        public void AdicionarListaContratoRelatorioPanorama(List<ContratoRelatorio> listaEntidade)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var listaSemRepeticao = listaEntidade.GroupBy(x => x.Contrato).Select(x => x.First());

                foreach (var entidade in listaSemRepeticao)
                {
                    var jaExiste = _unitOfWork.ContratoRelatorio.Listar(x => x.Contrato == entidade.Contrato);

                    if (jaExiste.Count == 0)
                    {
                        var pagamentosJaExiste = _unitOfWork.ContratoRelatorioPagamento.Listar(x => x.Contrato == entidade.Contrato && !x.ContratoRelatorioId.HasValue);

                        if (pagamentosJaExiste.Count > 0)
                        {
                            entidade.ContratoRelatorioPagamento = pagamentosJaExiste;
                        }

                        _unitOfWork.ContratoRelatorio.Adicionar(entidade);
                    }
                }

                _unitOfWork.Salvar();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
            }
        }

        public void AdicionarListaContratoRelatorioPagamento(List<ContratoRelatorioPagamento> listaEntidade)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                foreach (var entidade in listaEntidade)
                {
                    var contratos = _unitOfWork.ContratoRelatorio.Listar(x => x.Contrato == entidade.Contrato && x.Exportado == false);

                    if (contratos.Count > 0)
                    {
                        entidade.ContratoRelatorio = contratos.FirstOrDefault();
                    }

                    _unitOfWork.ContratoRelatorioPagamento.Adicionar(entidade);
                }

                _unitOfWork.Salvar();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
            }
        }

        public DataResponse<ImportarPlanilhaDto> ListarContratosArquivoPanorama(string caminho)
        {
            ILeitorArquivo leitor = new LeitorArquivo(caminho);
            var response = new DataResponse<ImportarPlanilhaDto>();
            var respostaDto = new ImportarPlanilhaDto();
            var leitorEntidades = new LeitorContratoPanoramaArquivo(leitor);

            leitorEntidades.PulaCabecalho();
            while (!leitorEntidades.EhFimArquivo)
            {
                leitorEntidades.LeConfigProximaLinha();
            }
            leitor.FecharLeitor();

            respostaDto.Contratos = leitorEntidades.Contratos;
            respostaDto.ContratosErro = leitorEntidades.LinhasComErro;

            response.Success = (leitorEntidades.LinhasComErro.Count == 0);
            response.Data = respostaDto;

            return response;
        }

        public DataResponse<ImportarPlanilhaPagamentosDto> ListarContratosArquivoPagamentos(TipoPlanilhaEnum tipoPlanilha, string caminho, string nomeArquivo)
        {
            ILeitorArquivo leitor = new LeitorArquivo(caminho);
            var response = new DataResponse<ImportarPlanilhaPagamentosDto>();
            var respostaDto = new ImportarPlanilhaPagamentosDto();
            ILeitorArquivoContratoPagamento leitorEntidades = null;

            if (TipoPlanilhaEnum.Corretora == tipoPlanilha)
            {
                leitorEntidades = new LeitorContratoCorretoraArquivo(leitor);
            }
            else if (TipoPlanilhaEnum.Banco == tipoPlanilha)
            {
                leitorEntidades = new LeitorContratoBancoSafraArquivo(leitor);
            }
            else
            {
                leitorEntidades = new LeitorContratoOutrosArquivo(leitor, nomeArquivo);
            }

            leitorEntidades.PulaCabecalho();
            while (!leitorEntidades.EhFimArquivo)
            {
                leitorEntidades.LeConfigProximaLinha();
            }
            leitor.FecharLeitor();

            respostaDto.Contratos = leitorEntidades.Contratos;
            respostaDto.ContratosErro = leitorEntidades.LinhasComErro;

            response.Success = (leitorEntidades.LinhasComErro.Count == 0);
            response.Data = respostaDto;

            return response;
        }

        public List<ContratoRelatorioDto> ListarContratosPorMes(bool comPagamentos, bool exportado)
        {
            try
            {
                var listaRetorno = new List<ContratoRelatorioDto>();
                List<ContratoRelatorio> listaRelatorios = null;

                listaRelatorios = _unitOfWork.ContratoRelatorio.Listar(x => x.Id == x.Id 
                                                                            && (x.ContratoRelatorioPagamento.Any() == comPagamentos) 
                                                                            && x.Exportado == exportado);

                foreach (var item in listaRelatorios)
                {
                    listaRetorno.Add(new ContratoRelatorioDto()
                    {
                        Id = item.Id,
                        Cpf = item.Cpf,
                        Banco = item.Banco,
                        BancoCredor = item.BancoCredor,
                        Contrato = item.Contrato,
                        DataLancamento = item.DataLancamento,
                        NomeAssessor = item.NomeAssessor,
                        NomeCliente = item.NomeCliente,
                        PercentualComissao = item.PercentualComissao,
                        PercentualComissaoCalculado = item.ContratoRelatorioPagamento.Sum(x => x.PercentualComissao),
                        Tabela = item.Tabela,
                        TabelaComissaoId = item.TabelaComissaoId,
                        TarefaExecucaoStatus = item.TarefaExecucaoStatus,
                        ValorCalculo = item.ValorCalculo,
                        ValorEmprestimo = item.ValorEmprestimo,
                        Exportado = exportado
                    });
                }

                return listaRetorno;
            }
            catch
            {
                return new List<ContratoRelatorioDto>();
            }
        }

        public List<ContratoRelatorioPagamento> ListarPagamentosDeContrato(int contrato)
        {
            try
            {
                return _unitOfWork.ContratoRelatorioPagamento.Listar(x => x.Contrato == contrato);
            }
            catch
            {
                return new List<ContratoRelatorioPagamento>();
            }
        }

        public List<ContratoRelatorioPagamento> ListarPagamentosSemContrato()
        {
            try
            {
                return _unitOfWork.ContratoRelatorioPagamento.Listar(x => !x.ContratoRelatorioId.HasValue);
            }
            catch
            {
                return new List<ContratoRelatorioPagamento>();
            }
        }

    }
}