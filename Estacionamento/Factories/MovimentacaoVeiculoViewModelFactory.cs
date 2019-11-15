using System;
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
        private readonly IValidator<EntradaVeiculoViewModel> _validatorEntrada;
        private readonly IValidator<SaidaVeiculoViewModel> _validatorSaida;
        private readonly IMovimentacaoService _movimentacaoService;
        private readonly ITabelaPrecosService _tabelaPrecosService;
        private readonly IVeiculoServices _veiculoServices;
        public MovimentacaoVeiculoViewModelFactory(IValidator<EntradaVeiculoViewModel> validator, 
            IMovimentacaoService movimentacaoService,
            ITabelaPrecosService tabelaPrecosService,
            IVeiculoServices veiculoServices,
            IValidator<SaidaVeiculoViewModel> validatorSaida)
        {
            _validatorEntrada = validator;
            _movimentacaoService = movimentacaoService;
            _tabelaPrecosService = tabelaPrecosService;
            _veiculoServices = veiculoServices;
            _validatorSaida = validatorSaida;
        }
        public EntradaVeiculoViewModel CreateEntradaViewModel()
        {
            return new EntradaVeiculoViewModel();
        }

        private async Task InternalPrepareListaMovimentacaoViewModel(ListaMovimentacaoViewModel viewModel)
        {
            var query = _movimentacaoService.GetQuery();
            query =  query.Include("Veiculo").Include("TabelaPreco");
            if (viewModel.DataInicio.HasValue)
                query = query.Where(d => d.Entrada >= viewModel.DataInicio);
            if (viewModel.DataFinal.HasValue)
                query = query.Where(d => d.Entrada <= viewModel.DataFinal);
            var results = await query.ToListAsync();
            viewModel.Movimentacoes = results.Select(s=> new MovimentacaoViewModel(s)).ToList();
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
            var results = _validatorEntrada.Validate(viewModel);
            if (!results.IsValid)
                throw new ModelValidateException("Não foi possível registrar a entrada!", results);

            var tab = await _tabelaPrecosService.GetQuery().Where(w => viewModel.DataEntrada >= w.Inicio && viewModel.DataEntrada <= w.Fim).FirstOrDefaultAsync();
            var veiculo = await _veiculoServices.GetQuery().Where(v => v.Id == viewModel.VeiculoId).FirstOrDefaultAsync();
            var movimentacao = new Movimentacao()
            {
                Entrada = viewModel.DataEntrada,
                VeiculoId = viewModel.VeiculoId,
                //TabelaPreco = tab,
                TabelaPrecoId = tab.Id,
                Valor = tab.Preco,
                //Veiculo = veiculo
            };
            await _movimentacaoService.CreateAsync(movimentacao);

        }

        public SaidaVeiculoViewModel PrepareSaidaVeiculoViewModel(int idMovimentacao)
        {
            var mov = _movimentacaoService.GetQuery().Include("Veiculo").Include("TabelaPreco").Where(m => m.Id == idMovimentacao).FirstOrDefault();
            if (mov == null)
                throw new ArgumentException($"Não foi encontrado movimentação com o id {idMovimentacao}", nameof(idMovimentacao));
            return PrepareSaidaVeiculoViewModel(mov);
        }

        public async Task SaveSaidaAsync(int id, DateTime dataSaida)
        {
            var mov = _movimentacaoService.CalculaPermanencia(id, dataSaida);
            var viewModel = PrepareSaidaVeiculoViewModel(mov);
            var results = _validatorSaida.Validate(viewModel);
            if (!results.IsValid)
                throw new ModelValidateException("Não foi possível registrar a entrada!", results);
            await _movimentacaoService.UpdateAsync(mov);
        }

        public SaidaVeiculoViewModel PrepareSaidaVeiculoViewModel(Movimentacao movimentacao)
        {
            SaidaVeiculoViewModel viewModel = new SaidaVeiculoViewModel();
            if (!movimentacao.Saida.HasValue)
            {
                viewModel.Saida = DateTime.Now;
                movimentacao.Saida = viewModel.Saida;
                _movimentacaoService.CalculaPermanencia(movimentacao);
            }
            SetSaidaVeiculoViewModel(movimentacao, ref viewModel);
            return viewModel;
        }

        private void SetSaidaVeiculoViewModel(Movimentacao mov, ref SaidaVeiculoViewModel viewModel)
        {
            viewModel.Id = mov.Id;
            viewModel.Entrada = mov.Entrada;
            viewModel.Saida = mov.Saida ?? DateTime.Now;
            viewModel.Veiculo = $"{mov.Veiculo.Placa}- {mov.Veiculo.Descricao}";
            viewModel.Permanencia = (viewModel.Saida - viewModel.Entrada).ToString("hh\\:mm");
            viewModel.Quantidade = mov.Quantidade ?? 0;
            viewModel.ValorHora = mov.Valor ?? 0;
            viewModel.Total = mov.ValorTotal ?? 0;
            viewModel.Mensagem = string.Empty;
        }

    }
}
