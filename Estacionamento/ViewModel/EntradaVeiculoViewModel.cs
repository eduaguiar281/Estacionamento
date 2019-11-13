using Estacionamento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.ViewModel
{
    public class EntradaVeiculoViewModel
    {
        public EntradaVeiculoViewModel()
        {
            DataEntrada = DateTime.Now;
        }

        public DateTime DataEntrada { get; set; }

        public string PlacaVeiculo { get; set; }

        public int VeiculoId { get; set; }

        public string Descricao { get; set; }

    }
}
