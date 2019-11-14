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

        public SaidaVeiculoViewModel CreateSaidaVeiculoViewModel(int idMovimentacao)
        {
            return new SaidaVeiculoViewModel(idMovimentacao, _movimentacaoService);
        }

        public async Task SaveSaidaAsync(SaidaVeiculoViewModel viewModel)
        {
            var results = _validatorSaida.Validate(viewModel);
            if (!results.IsValid)
                throw new ModelValidateException("Não foi possível registrar a entrada!", results);
            var mov = await _movimentacaoService.GetQuery().Where(x => x.Id == viewModel.Id).FirstOrDefaultAsync();
            mov.Saida = viewModel.Saida;
            mov.Quantidade = viewModel.Quantidade;
            mov.ValorTotal = viewModel.Total;
            await _movimentacaoService.UpdateAsync(mov);

        }
    }
}
