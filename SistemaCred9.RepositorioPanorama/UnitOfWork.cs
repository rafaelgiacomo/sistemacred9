using SistemaCred9.RepositorioPanorama.Repositorio;
using System;

namespace SistemaCred9.RepositorioPanorama
{
    public class UnitOfWork : IDisposable
    {

        private readonly string _connectionString;
        private readonly Context _context;

        #pragma warning disable 649
        private BeneficioRepositorio _beneficioRepositorio;
        private EmprestimoRepositorio _emprestimoRepositorio;
        private TarefaRepositorio _tarefaRepositorio;
        private StatusTarefaRepositorio _statusTarefaRepositorio;
        private ContratoRepositorio _contratoRepositorio;
        #pragma warning restore 649

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
            _context = new Context(connectionString);
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


        public BeneficioRepositorio Beneficios
        {
            get
            {
                if (_beneficioRepositorio == null)
                {
                    _beneficioRepositorio = new BeneficioRepositorio(_context);
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
                    _emprestimoRepositorio = new EmprestimoRepositorio(_context);
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
                    _tarefaRepositorio = new TarefaRepositorio(_context);
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
                    _statusTarefaRepositorio = new StatusTarefaRepositorio(_context);
                }
                return _statusTarefaRepositorio;
            }
        }

        public ContratoRepositorio Contratos
        {
            get
            {
                if (_contratoRepositorio == null)
                {
                    _contratoRepositorio = new ContratoRepositorio(_context);
                }
                return _contratoRepositorio;
            }
        }


    }
}
