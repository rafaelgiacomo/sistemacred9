using AutoMapper;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.TabelaComissao;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    public class TabelaComissaoController : BaseController
    {
        private readonly TabelaComissaoNegocio _tabelaComissaoNegocio;

        public TabelaComissaoController()
        {
            _tabelaComissaoNegocio = new TabelaComissaoNegocio(_unitOfWork);
        }

        [PermissoesFiltro(Roles = Role.TABELA_COMISSAO_INDEX)]
        public ActionResult Index()
        {
            var viewModel = new TabelaComissaoIndexViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.ListaTabelaComissao = Mapper.Map<List<TabelaComissaoViewModel>>(_tabelaComissaoNegocio.ListarTodos());
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.TABELA_COMISSAO_ADICIONAR)]
        public ActionResult Adicionar()
        {
            var viewModel = new TabelaComissaoViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Adicionar(TabelaComissaoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<TabelaComissao>(viewModel);
                _tabelaComissaoNegocio.Adicionar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível adicionar a Tabela.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.TABELA_COMISSAO_EDITAR)]
        public ActionResult Editar(int entidadeId)
        {
            var entidade = _tabelaComissaoNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<TabelaComissaoViewModel>(entidade);
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Editar(TabelaComissaoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<TabelaComissao>(viewModel);
                _tabelaComissaoNegocio.Atualizar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível atualizar dados da Tabela.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.TABELA_COMISSAO_EXCLUIR)]
        public ActionResult Excluir(int entidadeId)
        {
            var entidade = _tabelaComissaoNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<TabelaComissaoViewModel>(entidade);
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int entidadeId)
        {
            try
            {
                _tabelaComissaoNegocio.Deletar(entidadeId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["mensagem"] = ex.Message;
                return RedirectToAction("Erro");
            }
        }
    }
}