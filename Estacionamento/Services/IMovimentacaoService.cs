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
        Task<IList<Movimentacao>> GetMovimentacaoAsync();
        Task<IList<Movimentacao>> GetMovimentacaoAsync(Expression<Func<Movimentacao, bool>> predicate);
        Task CreateAsync(Movimentacao data);
        Task UpdateAsync(Movimentacao data);
        Task DeleteAsync(Movimentacao data);
        IQueryable<Movimentacao> GetQuery();
        void CalculaPermanencia(Movimentacao movimentacao);
        Movimentacao CalculaPermanencia(int id, DateTime dataSaida);
    }
}
