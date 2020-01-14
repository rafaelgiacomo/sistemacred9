using SistemaCred9.EntityFramework.Context;
using SistemaCred9.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SistemaCred9.Repositorio.Base
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : EntitadeBase
    {
        protected readonly Cred9DbContext DbContext;
        protected readonly DbSet<TEntidade> DbSet;

        public RepositorioBase(Cred9DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntidade>();
        }

        public TEntidade Adicionar(TEntidade entidade)
        {
            return DbSet.Add(entidade);
        }

        public IEnumerable<TEntidade> Adicionar(IEnumerable<TEntidade> entidades)
        {
            return DbSet.AddRange(entidades);
        }

        public void Atualizar(TEntidade entidade)
        {
            DbSet.Attach(entidade);
            DbContext.Entry(entidade).State = EntityState.Modified;
        }

        public TEntidade Deletar(int id)
        {
            var entidade = Obter(id);

            if (entidade != null)
            {
                return DbSet.Remove(entidade);
            }

            return null;
        }

        public bool Existe(int id)
        {
            return DbSet.Any(x => x.Id == id);
        }

        public List<TEntidade> Listar(Expression<Func<TEntidade, bool>> predicate)
        {
            return DbSet.Where(predicate).ToList();
        }

        public TEntidade Obter(int id)
        {
            return DbSet.Find(id);
        }

        public virtual int Quantidade(Expression<Func<TEntidade, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }
    }
}
