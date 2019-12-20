﻿using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Repositorio.Repositorios;
using SistemaCred9.Repositorio.Repositorios.Interfaces;
using System;
using System.Threading.Tasks;

namespace SistemaCred9.Repositorio.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Propriedades e Contrutores

        private Cred9DbContext _dbContext;

        public UnitOfWork(Cred9DbContext context)
        {
            _dbContext = context;
        }

        #endregion

        #region Repositorios

        #region private

        private IUsuarioRepositorio _usuarioRepositorio;
        private IRoleRepositorio _roleRepositorio;
        private IVendaRepositorio _vendaRepositorio;
        private IVendaStatusHistoricoRepositorio _vendaStatusHistoricoRepositorio;

        #endregion

        #region public 

        public IRoleRepositorio Role =>
            _roleRepositorio ?? (_roleRepositorio =
                new RoleRepositorio(_dbContext));

        public IUsuarioRepositorio Usuario =>
            _usuarioRepositorio ?? (_usuarioRepositorio =
                new UsuarioRepositorio(_dbContext));

        public IVendaRepositorio Venda =>
            _vendaRepositorio ?? (_vendaRepositorio =
                new VendaRepositorio(_dbContext));

        public IVendaStatusHistoricoRepositorio VendaStatusHistorico =>
            _vendaStatusHistoricoRepositorio ?? (_vendaStatusHistoricoRepositorio =
                new VendaStatusHistoricoRepositorio(_dbContext));

        #endregion

        #endregion

        #region Operacoes

        public int Salvar()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SalvarAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                _dbContext.Database.CurrentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                _dbContext.Database.CurrentTransaction.Rollback();
        }

        #endregion

        #region Dispose

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                    _dbContext?.Dispose();
                    _dbContext = null;
                }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
