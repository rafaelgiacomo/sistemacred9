using SistemaCred9.Modelo.Panorama;
using SistemaCred9.Repositorio.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCred9.Negocio
{
    public class TarefaNegocio
    {
        private readonly SistemaCred9.RepositorioPanorama.UnitOfWork _unitOfWorkPanorama;
        private readonly UnitOfWork _unitOfWork;

        public TarefaNegocio(SistemaCred9.RepositorioPanorama.UnitOfWork unitOfWorkPanorama, UnitOfWork unitOfWork)
        {
            _unitOfWorkPanorama = unitOfWorkPanorama;
            _unitOfWork = unitOfWork;
        }

        public StatusTarefa SelecionarStatusTarefa(int statusTarefaId)
        {
            return _unitOfWorkPanorama.StatusTarefa.Selecionar(statusTarefaId);
        }

        public List<StatusTarefa> ListarStatusTarefa(int tarefaId)
        {
            var listaCompleta = _unitOfWorkPanorama.StatusTarefa.ListarStatusTarefas(tarefaId);
            var listaComContrato = new List<StatusTarefa>();

            foreach (var item in listaCompleta)
            {
                if (_unitOfWorkPanorama.Contratos.QtdContratosPorStatus(tarefaId, item.Id) > 0)
                {
                    listaComContrato.Add(item);
                }
            }

            return listaComContrato;
        }

        public List<StatusTarefa> ListarStatusTarefaParaMudanca(int tarefaId, int statusTarefaId)
        {
            return _unitOfWorkPanorama.StatusTarefa.ListarStatusTarefasDiposniveisParaMudar(tarefaId, statusTarefaId);
        }

        public void AssumirContrato(int usuarioId, Contrato contrato)
        {
            contrato.UsuarioId = usuarioId;

            _unitOfWork.Contrato.Adicionar(contrato);
            _unitOfWork.Salvar();
        }

        public void DevolverContrato(int usuarioId, Contrato contrato)
        {
            contrato.UsuarioId = usuarioId;

            var entidade = _unitOfWork.Contrato.Listar(x => x.UsuarioId == usuarioId && x.NumContrato == contrato.NumContrato).FirstOrDefault();

            _unitOfWork.Contrato.Deletar(entidade.Id);
            _unitOfWork.Salvar();
        }

        public List<Contrato> ListarContratosSemDonoPorStatus(int tarefaId, int statusTarefaId)
        {
            var listaCompleta = _unitOfWorkPanorama.Contratos.ListaContratosPorStatus(tarefaId, statusTarefaId);
            var listaRetorno = new List<Contrato>();

            foreach (var item in listaCompleta)
            {
                if (_unitOfWork.Contrato.Listar(x => x.NumContrato == item.NumContrato).Count == 0)
                {
                    listaRetorno.Add(item);
                }
            }

            return listaRetorno;
        }

        public List<Contrato> ListarContratosComDonoPorStatus(int tarefaId, int statusTarefaId)
        {
            var listaCompleta = _unitOfWorkPanorama.Contratos.ListaContratosPorStatus(tarefaId, statusTarefaId);
            var listaRetorno = new List<Contrato>();

            foreach (var item in listaCompleta)
            {
                var contrato = _unitOfWork.Contrato.Listar(x => x.NumContrato == item.NumContrato);

                if (contrato.Count > 0)
                {
                    listaRetorno.Add(contrato.FirstOrDefault());
                }
            }

            return listaRetorno;
        }

        public List<Contrato> ListarContratosDoUsuarioPorStatus(int tarefaId, int statusTarefaId, int usuarioId)
        {
            var listaCompleta = _unitOfWorkPanorama.Contratos.ListaContratosPorStatus(tarefaId, statusTarefaId);
            var listaRetorno = new List<Contrato>();

            foreach (var item in listaCompleta)
            {
                var contrato = _unitOfWork.Contrato.Listar(x => x.NumContrato == item.NumContrato && x.UsuarioId == usuarioId);

                if (contrato.Count > 0)
                {
                    listaRetorno.Add(contrato.FirstOrDefault());
                }
            }

            return listaRetorno;
        }

    }
}
