using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCred9.Negocio
{
    public class VendaNegocio
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendaNegocio(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Adicionar(Venda entidade)
        {
            _unitOfWork.Venda.Adicionar(entidade);
            _unitOfWork.Salvar();
        }

        public void Excluir(int id)
        {
            var hist = _unitOfWork.VendaStatusHistorico.Listar(x => x.VendaId == id);

            foreach (var item in hist)
            {
                _unitOfWork.VendaStatusHistorico.Deletar(item.Id);
            }

            _unitOfWork.Venda.Deletar(id);

            _unitOfWork.Salvar();
        }

        public Venda Obter(int id)
        {
            return _unitOfWork.Venda.Obter(id);
        }

        public List<Venda> ListarVendasPorStatus(int status, int? usuarioId)
        {
            return _unitOfWork.Venda.Listar(x => x.StatusId == status && (usuarioId == null|| x.UsuarioId == usuarioId.Value));
        }

        public void AdicionarStatus(VendaStatusHistorico statusHistorico)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var venda = _unitOfWork.Venda.Obter(statusHistorico.VendaId);
                venda.StatusId = statusHistorico.StatusId;

                _unitOfWork.VendaStatusHistorico.Adicionar(statusHistorico);
                _unitOfWork.Venda.Atualizar(venda);

                _unitOfWork.Salvar();

                _unitOfWork.CommitTransaction();
            }
            catch 
            {
                _unitOfWork.RollbackTransaction();
            }
        }

        public int[] ListarQtdsVendaPorStatus(int? usuarioId)
        {
            int[] retorno = new int[15];

            retorno[0] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int) VendaStatusEnum.AguardandoDocumento 
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[1] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.DocumentoPendente
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[2] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.CanceladoFaltaDocumento
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[3] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.DocumentoOk
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[4] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.AguardandoHiscon
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[5] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.HisconOk
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[6] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.AguardandoCalculo
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[7] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.CanceladoPorCalculo
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[8] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.CalculoOk
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[9] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.RepassandoProposta
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[10] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.CanceladoRepasseProposta
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[11] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.PosVenda
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[12] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.PosVendaOK
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[13] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.RegistroProposta
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));
            retorno[14] = _unitOfWork.Venda.Quantidade(x => x.StatusId == (int)VendaStatusEnum.PropostaRegistrada
                                                            && (usuarioId == null || x.UsuarioId == usuarioId.Value));

            return retorno;
        }
    }
}
