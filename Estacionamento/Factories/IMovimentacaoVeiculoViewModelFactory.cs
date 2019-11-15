using Estacionamento.Entities;
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

        SaidaVeiculoViewModel PrepareSaidaVeiculoViewModel(int idMovimentacao);

        SaidaVeiculoViewModel PrepareSaidaVeiculoViewModel(Movimentacao movimentacao);

        Task SaveEntradaAsync(EntradaVeiculoViewModel viewModel);

        Task SaveSaidaAsync(int id, DateTime dataSaida);

        Task<ListaMovimentacaoViewModel> CreateListaMovimentacaoViewModelAsync();

        Task PrepareListaMovimentacaoViewModelAsync(ListaMovimentacaoViewModel listaMovimentacaoView);
    }
}
