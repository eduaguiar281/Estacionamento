using Estacionamento.Entities;
using Estacionamento.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Factories
{
    public interface ITabelaPrecoViewModelFactory
    {
        Task<TabelaPrecosViewModel> PrepareViewModelAsync();

        Task<TabelaPreco> PrepareToCreateAsync();
    }
}
