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
        Task SaveEntradaAsync(EntradaVeiculoViewModel viewModel);

        Task<ListaMovimentacaoViewModel> CreateListaMovimentacaoViewModelAsync();

        Task PrepareListaMovimentacaoViewModelAsync(ListaMovimentacaoViewModel listaMovimentacaoView);
    }
}
