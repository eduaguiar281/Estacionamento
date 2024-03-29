﻿using System;
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

        public void CalculaPermanencia(Movimentacao movimentacao)
        {
            if (!movimentacao.Saida.HasValue)
                return;
            //movimentacao.Duracao = (DataSaida.Value - DataEntrada);

            double totalMinutos = Math.Round((movimentacao.Saida.Value - movimentacao.Entrada).TotalMinutes,0);
            int qtHoras = Convert.ToInt32(Math.Truncate(totalMinutos / 60));
            var resto = totalMinutos % 60;
            if (movimentacao.TabelaPreco.ToleranciaMinutos < resto)
                qtHoras++;
            movimentacao.Quantidade = qtHoras;
            movimentacao.ValorTotal = movimentacao.Valor * qtHoras;
        }

        public Movimentacao CalculaPermanencia(int id, DateTime dataSaida)
        {
            var mov = _repository.Table.Include("Veiculo").Include("TabelaPreco").Where(m => m.Id == id).FirstOrDefault();
            if (mov == null)
                throw new ArgumentException($"Não foi encontrado movimentação com o id {id}", nameof(id));
            mov.Saida = dataSaida;
            CalculaPermanencia(mov);
            return mov;
        }
    }
}
