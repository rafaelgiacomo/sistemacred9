using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using SistemaCred9.RepositorioPanorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.Negocio
{
    public class ClienteNegocio
    {
        private readonly string _connectionString;
        private readonly ILogger _log;

        public ClienteNegocio(string connectionString, ILogger log)
        {
            _connectionString = connectionString;
            _log = log;
        }

        public void BuscaContatoCliente(Cliente cliente)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    unit.Clientes.BuscaContatoCliente(cliente);
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao buscar contato de cliente");
                _log.EscreveLinha("Classe ClienteBus - BuscaContatoCliente(ClienteModel cliente)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public List<Cliente> ListarClientesDentroPerfil(FiltroClientePanorama filtro)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    var lista = new HashSet<Cliente>(new ComparadorNomeCliente());

                    foreach (var f in filtro.FiltroBanco)
                    {
                        _log.EscreveLinha("RECUPERANDO CLIENTES DO BANCO: " + f.Banco);
                        _log.EscreveLinha("Minimo de parcela: " + f.MinParcela);
                        _log.EscreveLinha("Minimo de parcelas em aberto: " + f.MinParcelasEmAberto);
                        _log.EscreveLinha("Maximo de parcela em aberto: " + f.MaxParcelasEmAberto);
                        _log.EscreveLinha("Prazo: " + f.Prazo);

                        var listaCliente = unit.Clientes.ListarClientesDentroPerfil(f, filtro.MinDataNascimento, filtro.LimiteRegistros);

                        foreach (var c in listaCliente)
                        {
                            lista.Add(c);
                        }

                        _log.EscreveLinha(".......Clientes encontrados: " + lista.Count);
                        _log.PulaLinhas(1);
                    }

                    return new List<Cliente>(lista);
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao listar clientes dentro do perfil");
                _log.EscreveLinha("Classe ClienteBus - Método ListarClientesDentroPerfil(FiltroClienteModel filtro)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return new List<Cliente>();
            }
        }

        public List<Cliente> ListarClientesDeAgrupamento(int agrupamentoId)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    return unit.Clientes.ListarClientesDeAgrupamento(agrupamentoId);
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao listar clientes dentro do agrupamento ( " + agrupamentoId + " )");
                _log.EscreveLinha("Classe ClienteBus - Método ListarClientesDeAgrupamento(int agrupamentoId)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return new List<Cliente>();
            }
        }

        public List<Telefone> ListaTelefonesDoCliente(int clienteId)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    return unit.Telefones.ListarTelefonesDeCliente(clienteId);
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao listar telefones do cliente ( " + clienteId + " )");
                _log.EscreveLinha("Classe ClienteBus - Método ListaTelefonesDoCliente(int clienteId)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return new List<Telefone>();
            }
        }

        public void ConsultaEmprestimosClientes(List<Cliente> clientes)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    int porcentAnterior = 0;
                    for (int i = 0; i < clientes.Count; i++)
                    {
                        var cl = clientes[i];

                        float porcentagem = i / (float)clientes.Count;
                        porcentagem = (porcentagem * 100);

                        if ((int)porcentagem > porcentAnterior)
                        {
                            porcentAnterior = (int)porcentagem;
                            _log.EscreveLinha(porcentAnterior + " % dos emprestimos recuperados de " + clientes.Count);
                        }

                        foreach (var b in cl.Beneficios)
                        {
                            var emp = unit.Emprestimos.ConsultaEmprestimosPorBeneficioId(b.Id);
                            cl.Emprestimos.AddRange(emp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao consultar emprestimo de lista de clientes");
                _log.EscreveLinha("Classe ClienteBus - ConsultaEmprestimosClientes(List<ClienteModel> clientes)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public void ConsultaStatusClientes(List<Cliente> clientes)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    int porcentAnterior = 0;
                    for (int i = 0; i < clientes.Count; i++)
                    {
                        var cl = clientes[i];

                        float porcentagem = i / (float)clientes.Count;
                        porcentagem = (porcentagem * 100);

                        if ((int)porcentagem > porcentAnterior)
                        {
                            porcentAnterior = (int)porcentagem;
                            _log.EscreveLinha(porcentAnterior + " % dos status recuperados de " + clientes.Count);
                        }

                        cl.Status = unit.Status.ConsultaStatusDoCliente(cl.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao consultar status de lista de clientes");
                _log.EscreveLinha("Classe ClienteBus - ConsultaStatusClientes(List<ClienteModel> clientes)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public void ConsultaBeneficiosClientes(List<Cliente> clientes)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    int porcentAnterior = 0;
                    for (int i = 0; i < clientes.Count; i++)
                    {
                        var cl = clientes[i];

                        float porcentagem = i / (float)clientes.Count;
                        porcentagem = (porcentagem * 100);

                        if ((int)porcentagem > porcentAnterior)
                        {
                            porcentAnterior = (int)porcentagem;
                            _log.EscreveLinha(porcentAnterior + " % dos beneficios recuperados de " + clientes.Count);
                        }

                        cl.Beneficios = unit.Beneficios.ConsultaBeneficioPorClienteId(cl.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao consultar beneficios de lista de clientes");
                _log.EscreveLinha("Classe ClienteBus - ConsultaBeneficiosClientes(List<ClienteModel> clientes)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public void LimparBeneficiosInvalidos(List<Cliente> clientes, List<string> beneficiosInvalidos)
        {
            try
            {
                int porcentAnterior = 0;
                for (int i = 0; i < clientes.Count; i++)
                {
                    var cl = clientes[i];

                    float porcentagem = i / (float)clientes.Count;
                    porcentagem = (porcentagem * 100);

                    if ((int)porcentagem > porcentAnterior)
                    {
                        porcentAnterior = (int)porcentagem;
                        _log.EscreveLinha(porcentAnterior + " % dos beneficios validados de " + clientes.Count);
                    }

                    foreach (var b in cl.Beneficios)
                    {
                        if (!EhBeneficioValidio(b, beneficiosInvalidos))
                        {
                            cl.Ativado = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao limpar beneficios invalidos de lista de clientes");
                _log.EscreveLinha("Classe ClienteBus - LimparBeneficiosInvalidos(List<ClienteModel> clientes, List<string> beneficiosInvalidos)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public void LimparStatusInvalidos(List<Cliente> clientes)
        {
            try
            {
                int porcentAnterior = 0;
                for (int i = 0; i < clientes.Count; i++)
                {
                    var cl = clientes[i];

                    float porcentagem = i / (float)clientes.Count;
                    porcentagem = (porcentagem * 100);

                    if ((int)porcentagem > porcentAnterior)
                    {
                        porcentAnterior = (int)porcentagem;
                        _log.EscreveLinha(porcentAnterior + " % dos status validados de " + clientes.Count);
                    }

                    if (cl.Status != null && !EhStatusValido(cl.Status))
                    {
                        cl.Ativado = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao limpar status invalidos de lista de clientes");
                _log.EscreveLinha("Classe ClienteBus - LimparStatusInvalidos(List<ClienteModel> clientes)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public void LimparClientesForaDoPerfil(List<Cliente> clientes, FiltroClientePanorama filtro)
        {
            try
            {
                int porcentAnterior = 0;
                for (int i = 0; i < clientes.Count; i++)
                {
                    var cl = clientes[i];

                    float porcentagem = i / (float)clientes.Count;
                    porcentagem = (porcentagem * 100);

                    if ((int)porcentagem > porcentAnterior)
                    {
                        porcentAnterior = (int)porcentagem;
                        _log.EscreveLinha(porcentAnterior + " % dos clientes validados de " + clientes.Count);
                    }

                    if (!EhClienteDentroDoPerfil(cl, filtro))
                    {
                        cl.Ativado = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao limpar clientes fora de perfil de lista de clientes");
                _log.EscreveLinha("Classe ClienteBus - LimparClientesForaDoPerfil(List<ClienteModel> clientes, FiltroClienteModel filtro)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        #region Método privados

        private bool EhClienteDentroDoPerfil(Cliente c, FiltroClientePanorama filtro)
        {
            try
            {
                bool dentroPerfil = false;

                using (var unit = new UnitOfWork(_connectionString, _log))
                {
                    foreach (var e in c.Emprestimos)
                    {
                        foreach (var f in filtro.FiltroBanco)
                        {
                            double bruto = 0;
                            double liquido = 0;

                            if (!string.IsNullOrEmpty(e.Banco))
                            {
                                if (e.ValorParcela >= f.MinParcela
                                    && e.Prazo == f.Prazo
                                    && e.ParcelasEmAberto >= f.MinParcelasEmAberto
                                    && e.ParcelasEmAberto <= f.MaxParcelasEmAberto
                                    && e.Banco.Contains(f.Banco))
                                {
                                    dentroPerfil = true;
                                }

                                //bruto = e.ValorParcela / f.Coeficiente;
                                //liquido = bruto - e.Saldo;

                                //if (e.Saldo > 0
                                //    && liquido >= (e.Saldo * 0.05)
                                //    && e.ValorParcela >= f.MinParcela
                                //    && e.Prazo == f.Prazo
                                //    && e.ParcelasEmAberto >= f.MinParcelasEmAberto
                                //    && e.ParcelasEmAberto <= f.MaxParcelasEmAberto
                                //    && e.Banco.Contains(f.Banco))
                                //{
                                //    dentroPerfil = true;
                                //}
                            }

                            unit.LogFiltro.SalvarLog(new LogFiltro()
                            {
                                Emprestimo = e,
                                FiltroBanco = f,
                                ClienteId = c.Id,
                                Bruto = bruto,
                                Liquido = liquido,
                                Filtrado = dentroPerfil,
                                AgrupamentoId = filtro.CodAgrupamento,
                                Observacao = string.Empty
                            });
                        }
                    }
                }

                return dentroPerfil;
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao filtrar cliente");
                _log.EscreveLinha("Classe ClienteBus - bool EhClienteDentroDoPerfil(ClienteModel c, FiltroClienteModel filtro)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return false;
            }
        }

        private bool EhBeneficioValidio(Beneficio beneficio, List<string> beneficiosInvalidos)
        {
            foreach (var b in beneficiosInvalidos)
            {
                if (beneficio.Especie.ToString() == b)
                {
                    return false;
                }
            }
            return true;
        }

        private bool EhStatusValido(StatusTelemarketing statusCliente)
        {
            int[] validos = {
                StatusTelemarketing.TEL_INCORRETO, StatusTelemarketing.NAO_ATENDE, StatusTelemarketing.MUDO,
                StatusTelemarketing.CAIXA_POSTAL, StatusTelemarketing.LIG_CHAMANDO, StatusTelemarketing.TURNO_MANHA,
                StatusTelemarketing.TURNO_TARDE,
            };

            foreach (var status in validos)
            {
                if (statusCliente.Id == status)
                {
                    return true;
                }

                if (statusCliente.Id == StatusTelemarketing.SEM_INTERESSE_OUVIU)
                {
                    var intervalo = DateTime.Now - DateTime.Parse(statusCliente.Data);

                    if (intervalo.Days >= 15)
                        return true;
                }

                if (statusCliente.Id == StatusTelemarketing.SEM_INTERESSE_NAO_QUIS)
                {
                    var intervalo = DateTime.Now - DateTime.Parse(statusCliente.Data);

                    if (intervalo.Days >= 120)
                        return true;
                }

                if (statusCliente.Id == StatusTelemarketing.DIFICIL_LOCALIZACAO)
                {
                    var intervalo = DateTime.Now - DateTime.Parse(statusCliente.Data);

                    if (intervalo.Days >= 30)
                        return true;
                }

                if (statusCliente.Id == StatusTelemarketing.FECHADO
                    || statusCliente.Id == StatusTelemarketing.JA_FEZ)
                {
                    var intervalo = DateTime.Now - DateTime.Parse(statusCliente.Data);

                    if (intervalo.Days >= 90)
                        return true;
                }
            }

            return false;
        }

        #endregion
    }
}
