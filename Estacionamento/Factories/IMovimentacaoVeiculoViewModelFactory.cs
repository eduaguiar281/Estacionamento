using Estacionamento.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Factories
{
    public interface IMovimentacaoVeiculoViewModelFactory
    {
        EntradaVeiculoViewModel CreateEntradaViewModel();

        SaidaVeiculoViewModel CreateSaidaVeiculoViewModel(int idMovimentacao);
        
        Task SaveEntradaAsync(EntradaVeiculoViewModel viewModel);

        Task SaveSaidaAsync(SaidaVeiculoViewModel viewModel);

        Task<ListaMovimentacaoViewModel> CreateListaMovimentacaoViewModelAsync();

        Task PrepareListaMovimentacaoViewModelAsync(ListaMovimentacaoViewModel listaMovimentacaoView);
    }
}
