using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.Entities;
using Estacionamento.Services;
using Estacionamento.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Factories
{
    public class TabelaPrecoViewModelFactory : ITabelaPrecoViewModelFactory
    {
        private readonly ITabelaPrecosService _tabelaPrecoService;

        public TabelaPrecoViewModelFactory(ITabelaPrecosService tabelaPrecoService)
        {
            _tabelaPrecoService = tabelaPrecoService;
        }

        public async Task<TabelaPreco> PrepareToCreateAsync()
        {
            var result = new TabelaPreco
            {
                Inicio = DateTime.Now,
                Fim = DateTime.Now.AddMonths(12)
            };
            var tabelaPrecoAnterior = await _tabelaPrecoService.GetQuery().Where(x => x.Fim <= DateTime.Now).OrderByDescending(o => o.Fim).FirstOrDefaultAsync();
            if (tabelaPrecoAnterior == null)
                return result;

            result.Preco = tabelaPrecoAnterior.Preco;
            result.ToleranciaMinutos = tabelaPrecoAnterior.ToleranciaMinutos;
            return result;
        }

        public async Task<TabelaPrecosViewModel> PrepareViewModelAsync()
        {
            TabelaPrecosViewModel result = new TabelaPrecosViewModel();
            result.TabelaPrecos = await _tabelaPrecoService.GetTabelasAsync();
            return result;
        }
    }
}
