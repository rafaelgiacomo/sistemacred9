using SistemaCred9.Modelo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SistemaCred9.Repositorio.Base
{
    public interface IRepositorioBase<TEntidade> where TEntidade : EntitadeBase
    {
        TEntidade Obter(int id);
        List<TEntidade> Listar(Expression<Func<TEntidade, bool>> predicate);
        bool Existe(int id);
        TEntidade Adicionar(TEntidade entidade);
        IEnumerable<TEntidade> Adicionar(IEnumerable<TEntidade> entidades);
        void Atualizar(TEntidade entidade);
        TEntidade Deletar(int id);
        int Quantidade(Expression<Func<TEntidade, bool>> predicate);
    }
}
