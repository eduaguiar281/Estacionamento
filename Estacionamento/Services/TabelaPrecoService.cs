using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Estacionamento.Entities;
using Estacionamento.Exceptions;
using Estacionamento.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Services
{
    public class TabelaPrecoService :ITabelaPrecosService
    {

        private readonly IRepository<TabelaPreco> _repository;
        private readonly IValidator<TabelaPreco> _validator;
        public TabelaPrecoService(IRepository<TabelaPreco> repository, IValidator<TabelaPreco> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        private void ValidaTabela(TabelaPreco data)
        {
            var validationResults = _validator.Validate(data);
            if (!validationResults.IsValid)
            {
                throw new ModelValidateException("Tabela de Preço não é Válida!", validationResults);
            }
        }

        public async Task CreateAsync(TabelaPreco data)
        {
            ValidaTabela(data);
            await _repository.InsertAsync(data);
        }

        public async Task DeleteAsync(TabelaPreco data)
        {
            await  _repository.DeleteAsync(data);
        }

        public async Task<IList<TabelaPreco>> GetTabelasAsync()
        {
            return await _repository.Table.ToListAsync();
        }

        public async Task<IList<TabelaPreco>> GetTabelasAsync(Expression<Func<TabelaPreco, bool>> predicate)
        {
            return await _repository.Table.Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(TabelaPreco data)
        {
            ValidaTabela(data); 
            await _repository.UpdateAsync(data);
        }

        public IQueryable<TabelaPreco> GetQuery()
        {
            return _repository.Table;
        }
    }
}
