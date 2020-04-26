using AutoMapper;
using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Infra;
using SistemaCred9.Modelo;
using SistemaCred9.Negocio;
using SistemaCred9.Repositorio.UnitOfWork;
using SistemaCred9.Web.UI.Security;
using SistemaCred9.Web.UI.ViewModels.Filtro;
using System;
using System.Web.Mvc;

namespace SistemaCred9.Web.UI.Controllers
{
    [Authorize]
    public class FiltroController : BaseController
    {
        private readonly FiltroNegocio _filtroNegocio;
        private readonly FiltroBancoNegocio _bancoNegocio;
        private readonly FiltroEspecieNegocio _especieNegocio;
        private readonly AgrupamentoNegocio _agrupamentoNegocio;

        public FiltroController()
        {
            var unitOfWork = new UnitOfWork(new Cred9DbContext());
            var unitOfWorkPanorama = new RepositorioPanorama.UnitOfWork(_connectionString, new LoggerConsole());

            _filtroNegocio = new FiltroNegocio(unitOfWork);
            _bancoNegocio = new FiltroBancoNegocio(unitOfWork);
            _especieNegocio = new FiltroEspecieNegocio(unitOfWork);
            _agrupamentoNegocio = new AgrupamentoNegocio(_connectionString, new LoggerConsole());
        }

        [PermissoesFiltro(Roles = Role.FILTRO_INDEX)]
        public ActionResult Index()
        {
            var viewModel = new FiltroIndexViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            var listaFiltros = _filtroNegocio.ListarTodos();

            foreach (var item in listaFiltros)
            {
                var agp = _agrupamentoNegocio.Obter(item.CodAgrupamento);
                var itemVm = new ContratoRelatorioViewModel()
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Ativo = item.Ativo,
                    CodAgrupamento = item.CodAgrupamento,
                    AgrupamentoDescricao = (agp != null) ? item.CodAgrupamento + " - " + agp.Descricao : "Nenhum agrupamento",
                    LimiteRegistros = item.LimiteRegistros,
                    MinDataNascimento = item.MinDataNascimento
                };

                itemVm.BancosSelecionados = item.ListaFiltroBanco != null ? new string[item.ListaFiltroBanco.Count] : new string[0];
                itemVm.EspeciesSelecionadas = item.ListaFiltroEspecie != null ? new string[item.ListaFiltroEspecie.Count] : new string[0];

                int i = 0;
                foreach (var b in item.ListaFiltroBanco)
                {
                    itemVm.BancosSelecionados[i] = b.Banco;
                    i++;
                }

                i = 0;
                foreach (var e in item.ListaFiltroEspecie)
                {
                    itemVm.EspeciesSelecionadas[i] = e.Descricao;
                    i++;
                }

                viewModel.Filtros.Add(itemVm);
            }

            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);

            return View(viewModel);
        }

        [PermissoesFiltro(Roles = Role.FILTRO_ADICIONAR)]
        public ActionResult Adicionar()
        {
            var viewModel = new ContratoRelatorioViewModel();
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.BancosDisponiveis = _bancoNegocio.ListarTodos();
            viewModel.EspeciesDisponiveis = _especieNegocio.ListarTodos();
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);
            viewModel.ListaAgrupamentos = _agrupamentoNegocio.ListarAgrupamentos();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Adicionar(ContratoRelatorioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<Filtro>(viewModel);

                foreach (var item in viewModel.BancosSelecionados)
                {
                    entidade.ListaFiltroBanco.Add(_bancoNegocio.SelecionarPorId(int.Parse(item)));
                }

                foreach (var e in viewModel.EspeciesSelecionadas)
                {
                    entidade.ListaFiltroEspecie.Add(_especieNegocio.SelecionarPorId(int.Parse(e)));
                }

                _filtroNegocio.Adicionar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível adicionar Espécie.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.FILTRO_EDITAR)]
        public ActionResult Editar(int entidadeId)
        {
            var entidade = _filtroNegocio.SelecionarPorId(entidadeId);
            var usuario = _usuarioNegocio.SelecionarPorLogin(User.Identity.Name);
            int? usuarioId = null;

            var viewModel = new ContratoRelatorioViewModel()
            {
                Id = entidade.Id,
                Descricao = entidade.Descricao,
                Ativo = entidade.Ativo,
                CodAgrupamento = entidade.CodAgrupamento,
                LimiteRegistros = entidade.LimiteRegistros,
                MinDataNascimento = entidade.MinDataNascimento
            };

            if (usuario.TipoUsuarioId == (int)TipoUsuarioEnum.Operador)
            {
                usuarioId = usuario.Id;
            }

            viewModel.BancosDisponiveis = _bancoNegocio.ListarTodos();
            viewModel.EspeciesDisponiveis = _especieNegocio.ListarTodos();
            viewModel.ArrayQtdPorStatus = _vendaNegocio.ListarQtdsVendaPorStatus(usuarioId);
            viewModel.ListaStatusTarefa = _tarefaNegocio.ListarStatusTarefa(31);
            viewModel.ListaAgrupamentos = _agrupamentoNegocio.ListarAgrupamentos();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Editar(ContratoRelatorioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var entidade = Mapper.Map<Filtro>(viewModel);
                _filtroNegocio.Atualizar(entidade);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível atualizar dados da Especie.";
                return RedirectToAction("Erro");
            }
        }

        [PermissoesFiltro(Roles = Role.FILTRO_EXCLUIR)]
        public ActionResult Excluir(int entidadeId)
        {
            var entidade = _filtroNegocio.SelecionarPorId(entidadeId);
            var viewModel = Mapper.Map<ContratoRelatorioViewModel>(entidade);
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
                _filtroNegocio.Deletar(entidadeId);
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