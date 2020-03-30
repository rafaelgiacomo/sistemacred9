using SistemaCred9.Infra.Interface;
using SistemaCred9.Modelo.Panorama;
using SistemaCred9.RepositorioPanorama;
using System;
using System.Collections.Generic;

namespace SistemaCred9.Negocio
{
    public class AgrupamentoNegocio
    {
        private string _connectionString;
        private readonly ILogger _log;

        public AgrupamentoNegocio(string connectionString, ILogger log)
        {
            _connectionString = connectionString;
            _log = log;
        }

        public List<Agrupamento> ListarAgrupamentos()
        {
            using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
            {
                return unit.Agrupamentos.ListaAgrupamentos();
            }
        }

        public Agrupamento Obter(int id)
        {
            using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
            {
                return unit.Agrupamentos.Obter(id);
            }
        }

        public bool ConsultaClientePresente(int idCliente, int idAgrupamento)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    return unit.Agrupamentos.ConsultaClientePresente(idCliente, idAgrupamento);
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao consultar se o cliente ( " + idCliente + " ) está presente no agrupamento ( " + idAgrupamento + " )");
                _log.EscreveLinha("Classe AgrupamentoBus - Método ConsultaClientePresente(int idCliente, int idAgrupamento)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
                return false;
            }
        }

        public void AdicionarAgrupamento(Agrupamento entidade)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    unit.Agrupamentos.AdicionarAgrupamento(entidade);
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao adicionar o agrupamento ( " + entidade.Descricao + " )");
                _log.EscreveLinha("Classe AgrupamentoBus - Método AdicionarAgrupamento(AgrupamentoModel entidade)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

        public void AdicionarClienteNoAgrupamento(int idAgrupamento, int idCliente)
        {
            try
            {
                using (UnitOfWork unit = new UnitOfWork(_connectionString, _log))
                {
                    unit.Agrupamentos.AdicionarClienteNoAgrupamento(idAgrupamento, idCliente);
                }
            }
            catch (Exception ex)
            {
                _log.EscreveLinha("Erro ao adicionar o cliente ( " + idCliente + " ) no agrupamento ( " + idAgrupamento + " )");
                _log.EscreveLinha("Classe AgrupamentoBus - Método AdicionarClienteNoAgrupamento(int idAgrupamento, int idCliente)");
                _log.EscreveLinha("Mensagem do erro: " + ex.Message);
            }
        }

    }
}
