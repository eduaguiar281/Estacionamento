using Estacionamento.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Estacionamento.Services
{
    public interface IVeiculoServices
    {
        Task<IList<Veiculo>> GetTabelasAsync();
        Task<IList<Veiculo>> GetTabelasAsync(Expression<Func<Veiculo, bool>> predicate);

        Task CreateAsync(Veiculo data);

        Task UpdateAsync(Veiculo data);

        Task DeleteAsync(Veiculo data);

        IQueryable<Veiculo> GetQuery();
    }
}
