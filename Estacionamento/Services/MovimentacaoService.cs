using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Estacionamento.Entities;
using Estacionamento.Exceptions;
using Estacionamento.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        public IRepository<Movimentacao> _repository;
        public IValidator<Movimentacao> _validator;
        public MovimentacaoService(IRepository<Movimentacao> repository, IValidator<Movimentacao> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        private void ValidateMovimentacao(Movimentacao data)
        {
            var validationResults = _validator.Validate(data);
            if (!validationResults.IsValid)
            {
                throw new ModelValidateException("Movimentação não é válida!", validationResults);
            }
        }

        public async Task CreateAsync(Movimentacao data)
        {
            ValidateMovimentacao(data);
            await _repository.InsertAsync(data);
        }

        public async Task DeleteAsync(Movimentacao data)
        {
            await _repository.DeleteAsync(data);
        }

        public async Task<IList<Movimentacao>> GetMovimentacaoAsync()
        {
            return await _repository.Table.ToListAsync(); 
        }

        public async Task<IList<Movimentacao>> GetMovimentacaoAsync(Expression<Func<Movimentacao, bool>> predicate)
        {
            return await _repository.Table.Where(predicate).ToListAsync();
        }

        public IQueryable<Movimentacao> GetQuery()
        {
            return _repository.Table;
        }

        public async Task UpdateAsync(Movimentacao data)
        {
            ValidateMovimentacao(data);
            await _repository.UpdateAsync(data);
        }
    }
}
