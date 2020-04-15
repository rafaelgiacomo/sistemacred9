using SistemaCred9.Infra.Interface;
using SistemaCred9.RepositorioPanorama.Repositorio;
using System;

namespace SistemaCred9.RepositorioPanorama
{
    public class UnitOfWork : IDisposable
    {
        private readonly Context _context;
        private readonly ILogger _logger;

        #pragma warning disable 649
        private ClienteRepositorio _clienteRepositorio;
        private StatusTelemarketingRepositorio _statusRepositorio;
        private AgrupamentoRepositorio _agrupamentoRepositorio;
        private BeneficioRepositorio _beneficioRepositorio;
        private EmprestimoRepositorio _emprestimoRepositorio;
        private TelefoneRepositorio _telefoneRepositorio;
        private LogFiltroRepositorio _logFiltroRepositorio;
        private TarefaRepositorio _tarefaRepositorio;
        private StatusTarefaRepositorio _statusTarefaRepositorio;
        private ContratoRepositorio _contratoRepositorio;
        
        #pragma warning restore 649

        public UnitOfWork(string connectionString, ILogger logger)
        {
            _context = new Context(connectionString);
            _logger = logger;
            _context.AbrirConexao();
        }

        public void AbrirTransacao()
        {
            _context.OpenTransaction();
        }

        public void Commit()
        {
            _context.Commit();
        }

        public void RollBack()
        {
            _context.RollBack();
        }

        public void AbrirConexao()
        {
            _context.AbrirConexao();
        }

        public void FecharConexao()
        {
            _context.FecharConexao();
        }

        public void Dispose()
        {
            _context.FecharConexao();
        }

        public ClienteRepositorio Clientes
        {
            get
            {
                if (_clienteRepositorio == null)
                {
                    _clienteRepositorio = new ClienteRepositorio(_context, _logger);
                }
                return _clienteRepositorio;
            }
        }

        public AgrupamentoRepositorio Agrupamentos
        {
            get
            {
                if (_agrupamentoRepositorio == null)
                {
                    _agrupamentoRepositorio = new AgrupamentoRepositorio(_context, _logger);
                }
                return _agrupamentoRepositorio;
            }
        }

        public TelefoneRepositorio Telefones
        {
            get
            {
                if (_telefoneRepositorio == null)
                {
                    _telefoneRepositorio = new TelefoneRepositorio(_context, _logger);
                }
                return _telefoneRepositorio;
            }
        }

        public LogFiltroRepositorio LogFiltro
        {
            get
            {
                if (_logFiltroRepositorio == null)
                {
                    _logFiltroRepositorio = new LogFiltroRepositorio(_context, _logger);
                }
                return _logFiltroRepositorio;
            }
        }

        public BeneficioRepositorio Beneficios
        {
            get
            {
                if (_beneficioRepositorio == null)
                {
                    _beneficioRepositorio = new BeneficioRepositorio(_context, _logger);
                }
                return _beneficioRepositorio;
            }
        }

        public EmprestimoRepositorio Emprestimos
        {
            get
            {
                if (_emprestimoRepositorio == null)
                {
                    _emprestimoRepositorio = new EmprestimoRepositorio(_context, _logger);
                }
                return _emprestimoRepositorio;
            }
        }

        public TarefaRepositorio Tarefas
        {
            get
            {
                if (_tarefaRepositorio == null)
                {
                    _tarefaRepositorio = new TarefaRepositorio(_context, _logger);
                }
                return _tarefaRepositorio;
            }
        }

        public StatusTarefaRepositorio StatusTarefa
        {
            get
            {
                if (_statusTarefaRepositorio == null)
                {
                    _statusTarefaRepositorio = new StatusTarefaRepositorio(_context, _logger);
                }
                return _statusTarefaRepositorio;
            }
        }

        public StatusTelemarketingRepositorio Status
        {
            get
            {
                if (_statusRepositorio == null)
                {
                    _statusRepositorio = new StatusTelemarketingRepositorio(_context, _logger);
                }
                return _statusRepositorio;
            }
        }

        public ContratoRepositorio Contratos
        {
            get
            {
                if (_contratoRepositorio == null)
                {
                    _contratoRepositorio = new ContratoRepositorio(_context, _logger); ;
                }
                return _contratoRepositorio;
            }
        }


    }
}
