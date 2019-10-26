using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Estacionamento.Entities;

namespace Estacionamento.Services
{
    public class TabelaPrecoService :ITabelaPrecosService
    {
        public TabelaPrecoService()
        {

        }

        public async Task<TabelaPreco> CreateAsync(TabelaPreco data)
        {
            throw new NotImplementedException();
        }

        public async Task<TabelaPreco> DeleteAsync(TabelaPreco data)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TabelaPreco>> GetTabelasAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TabelaPreco>> GetTabelasAsync(Expression<Func<TabelaPreco, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<TabelaPreco> UpdateAsync(TabelaPreco data)
        {
            throw new NotImplementedException();
        }
    }
}
