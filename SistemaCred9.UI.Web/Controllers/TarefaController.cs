﻿using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.Tarefa;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class TarefaController : BaseController
    {
        public TarefaController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
        }

        [PermissoesFiltro(Roles = Role.TAREFA_INDEX)]
        public ActionResult Index(int? opcaoProprietarioSelecionado, int statusTarefaId)
        {
            var viewModel = new TarefaIndexViewModel();

            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            if (opcaoProprietarioSelecionado == null)
            {
                opcaoProprietarioSelecionado = 1;
            }

            if (opcaoProprietarioSelecionado == 1)
            {
                viewModel.Contratos = _tarefaNegocio.ListarContratosSemDonoPorStatus(31, statusTarefaId);
            }
            else if(opcaoProprietarioSelecionado == 2){
                viewModel.Contratos = _tarefaNegocio.ListarContratosComDonoPorStatus(31, statusTarefaId);
            }
            else
            {
                viewModel.Contratos = _tarefaNegocio.ListarContratosDoUsuarioPorStatus(31, statusTarefaId, usuario.Id);
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);
            viewModel.StatusMudanca = _tarefaNegocio.ListarStatusTarefaParaMudanca(31, statusTarefaId);
            viewModel.StatusIdAtual = statusTarefaId;
            viewModel.OpcaoProprietarioSelecionado = opcaoProprietarioSelecionado.Value;
            viewModel.StatusAtual = _tarefaNegocio.SelecionarStatusTarefa(statusTarefaId).Descricao;

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.TAREFA_ASSUMIR)]
        public ActionResult AssumirTarefa(int statusTarefaId, int numContrato)
        {
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            var contrato = _tarefaNegocio.ListarContratosSemDonoPorStatus(31, statusTarefaId).Where(x => x.NumContrato == numContrato).FirstOrDefault();

            if (contrato == null)
            {
                throw new Exception("Não foi possível encontrar o contrato informado. Tente Novamente!");
            }

            _tarefaNegocio.AssumirContrato(usuario.Id, contrato);

            return RedirectToAction("Index", new { opcaoProprietarioSelecionado = 3, statusTarefaId = statusTarefaId });
        }

        [PermissoesFiltro(Roles = Role.TAREFA_DEVOLVER)]
        public ActionResult DevolverTarefa(int statusTarefaId, int numContrato)
        {
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            var contrato = _tarefaNegocio.ListarContratosDoUsuarioPorStatus(31, statusTarefaId, usuario.Id).Where(x => x.NumContrato == numContrato).FirstOrDefault();
            if (contrato == null)
            {
                throw new Exception("Não foi possível encontrar o contrato informado. Tente Novamente!");
            }

            _tarefaNegocio.DevolverContrato(usuario.Id, contrato);

            return RedirectToAction("Index", new { opcaoProprietarioSelecionado = 1, statusTarefaId = statusTarefaId });
        }

        [PermissoesFiltro(Roles = Role.TAREFA_DEVOLVER)]
        public ActionResult MudarStatus(int statusTarefaId, int numContrato)
        {
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            var contrato = _tarefaNegocio.ListarContratosDoUsuarioPorStatus(31, statusTarefaId, usuario.Id).Where(x => x.NumContrato == numContrato).FirstOrDefault();

            if (contrato == null)
            {
                throw new Exception("Não foi possível encontrar o contrato informado. Tente Novamente!");
            }

            _tarefaNegocio.DevolverContrato(usuario.Id, contrato);

            return RedirectToAction("Index", new { opcaoProprietarioSelecionado = 1, statusTarefaId = statusTarefaId });
        }
    }
}