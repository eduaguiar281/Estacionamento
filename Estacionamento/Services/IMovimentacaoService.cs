using Estacionamento.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Estacionamento.Services
{
    public interface IMovimentacaoService
    {
        Task<IList<Movimentacao>> GetTabelasAsync();
        Task<IList<Movimentacao>> GetTabelasAsync(Expression<Func<Movimentacao, bool>> predicate);
        Task<Movimentacao> CreateAsync(Movimentacao data);
        Task<Movimentacao> UpdateAsync(Movimentacao data);
        Task<Movimentacao> DeleteAsync(Movimentacao data);

    }
}
