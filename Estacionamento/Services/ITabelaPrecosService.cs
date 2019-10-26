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
        Task<IList<TabelaPreco>> GetTabelasAsync();
        Task<IList<TabelaPreco>> GetTabelasAsync(Expression<Func<TabelaPreco, bool>> predicate);

        Task<TabelaPreco> CreateAsync(TabelaPreco data);

        Task<TabelaPreco> UpdateAsync(TabelaPreco data);

        Task<TabelaPreco> DeleteAsync(TabelaPreco data);

    }
}
