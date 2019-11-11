using Estacionamento.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Estacionamento.Services
{
    public interface ITabelaPrecosService
    {
        IQueryable<TabelaPreco> GetQuery();

        Task<IList<TabelaPreco>> GetTabelasAsync();
        Task<IList<TabelaPreco>> GetTabelasAsync(Expression<Func<TabelaPreco, bool>> predicate);

        Task CreateAsync(TabelaPreco data);

        Task UpdateAsync(TabelaPreco data);

        Task DeleteAsync(TabelaPreco data);

    }
}
