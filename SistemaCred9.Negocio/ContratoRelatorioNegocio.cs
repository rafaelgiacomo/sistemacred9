using SistemaCred9.Core.Dto;
using SistemaCred9.Core.Resposta;
using SistemaCred9.Infra;
using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
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

        public DataResponse<List<ContratoRelatorioErroDto>> RealizarImportacao(TipoPlanilhaEnum tipoPlanilha, string caminho)
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
                var response = ListarContratosArquivoPagamentos(tipoPlanilha, caminho);

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

                foreach (var entidade in listaEntidade)
                {
                    _unitOfWork.ContratoRelatorio.Adicionar(entidade);
                }

                _unitOfWork.Salvar();
                _unitOfWork.CommitTransaction();
            }
            catch(Exception ex)
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

        public DataResponse<ImportarPlanilhaPagamentosDto> ListarContratosArquivoPagamentos(TipoPlanilhaEnum tipoPlanilha, string caminho)
        {
            ILeitorArquivo leitor = new LeitorArquivo(caminho);
            var response = new DataResponse<ImportarPlanilhaPagamentosDto>();
            var respostaDto = new ImportarPlanilhaPagamentosDto();
            ILeitorArquivoContratoPagamento leitorEntidades = null;

            if (TipoPlanilhaEnum.Corretora == tipoPlanilha)
            {
                leitorEntidades = new LeitorContratoCorretoraArquivo(leitor);
            }
            else
            {
                leitorEntidades = new LeitorContratoCorretoraArquivo(leitor);
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

        public List<ContratoRelatorio> ListarContratosPorMes(int ano, int mes)
        {
            try
            {
                //var data = DateTime.Parse(ano.ToString() + "-" + mes.ToString() + "-01");

                return _unitOfWork.ContratoRelatorio.Listar(x => x.DataLancamento.HasValue && x.DataLancamento.Value.Year == ano && x.DataLancamento.Value.Month == mes);
            }
            catch
            {
                return new List<ContratoRelatorio>();
            }
        }
    }
}