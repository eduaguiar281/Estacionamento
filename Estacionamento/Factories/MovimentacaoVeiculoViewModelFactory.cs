using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.Entities;
using Estacionamento.Exceptions;
using Estacionamento.Services;
using Estacionamento.ViewModel;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Factories
{
    public class MovimentacaoVeiculoViewModelFactory : IMovimentacaoVeiculoViewModelFactory
    {
        private readonly IValidator<EntradaVeiculoViewModel> _validator;
        private readonly IMovimentacaoService _movimentacaoService;
        private readonly ITabelaPrecosService _tabelaPrecosService;
        public MovimentacaoVeiculoViewModelFactory(IValidator<EntradaVeiculoViewModel> validator, 
            IMovimentacaoService movimentacaoService,
            ITabelaPrecosService tabelaPrecosService)
        {
            _validator = validator;
            _movimentacaoService = movimentacaoService;
            _tabelaPrecosService = tabelaPrecosService;
        }
        public EntradaVeiculoViewModel CreateEntradaViewModel()
        {
            return new EntradaVeiculoViewModel();
        }

        private async Task InternalPrepareListaMovimentacaoViewModel(ListaMovimentacaoViewModel viewModel)
        {
            var query = _movimentacaoService.GetQuery();
            if (viewModel.DataInicio.HasValue)
                query = query.Where(d => d.Entrada >= viewModel.DataInicio);
            if (viewModel.DataFinal.HasValue)
                query = query.Where(d => d.Entrada <= viewModel.DataFinal);
            var results = await query.ToListAsync();
            viewModel.Movimentacoes = results.Select(s=> new MovimentacaoViewModel(s, true)).ToList();
        }
        public async Task<ListaMovimentacaoViewModel> CreateListaMovimentacaoViewModelAsync()
        {
            ListaMovimentacaoViewModel viewModel = new ListaMovimentacaoViewModel();
            await InternalPrepareListaMovimentacaoViewModel(viewModel);
            return viewModel;
        }

        public async Task PrepareListaMovimentacaoViewModelAsync(ListaMovimentacaoViewModel listaMovimentacaoView)
        {
            await InternalPrepareListaMovimentacaoViewModel(listaMovimentacaoView);
            
        }

        public async Task SaveEntradaAsync(EntradaVeiculoViewModel viewModel)
        {
            var results = _validator.Validate(viewModel);
            if (!results.IsValid)
                throw new ModelValidateException("Não foi possível registrar a entrada!", results);

            var tab = await _tabelaPrecosService.GetQuery().Where(w => viewModel.DataEntrada >= w.Inicio && viewModel.DataEntrada <= w.Fim).FirstOrDefaultAsync();

            var movimentacao = new Movimentacao()
            {
                Entrada = viewModel.DataEntrada,
                VeiculoId = viewModel.VeiculoId,
                TabelaPreco = tab,
                Valor = tab.Preco
            };
            await _movimentacaoService.CreateAsync(movimentacao);

        }
    }
}
