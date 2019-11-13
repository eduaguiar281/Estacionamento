using Estacionamento.Entities;
using Estacionamento.Exceptions;
using Estacionamento.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Estacionamento.Services
{
    public class VeiculoServices : IVeiculoServices
    {
        public IRepository<Veiculo> _repository;
        public IValidator<Veiculo> _validator;

        public VeiculoServices(IRepository<Veiculo> repository, IValidator<Veiculo> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        private void ValidateVeiculo(Veiculo data)
        {
            var validationResults = _validator.Validate(data);
            if (!validationResults.IsValid)
            {
                throw new ModelValidateException("Veículo não é válido!", validationResults);
            }
        }


        public async Task CreateAsync(Veiculo data)
        {
            ValidateVeiculo(data);
            await _repository.InsertAsync(data);
        }

        public async Task DeleteAsync(Veiculo data)
        {
            await _repository.DeleteAsync(data);
        }

        public IQueryable<Veiculo> GetQuery()
        {
            return _repository.Table;
        }

        public async Task<IList<Veiculo>> GetTabelasAsync()
        {
            return await _repository.Table.ToListAsync();
        }

        public async Task<IList<Veiculo>> GetTabelasAsync(Expression<Func<Veiculo, bool>> predicate)
        {
            return await _repository.Table.Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(Veiculo data)
        {
            ValidateVeiculo(data);
            await _repository.UpdateAsync(data);
        }
    }
}
