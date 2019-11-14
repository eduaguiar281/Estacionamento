using Estacionamento.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name ="Data de Entrada")]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "Placa")]
        public string PlacaVeiculo { get; set; }

        [Display(Name = "Id do Veículo")]
        public int VeiculoId { get; set; }

        [Display(Name = "Descrição Veículo")]
        public string Descricao { get; set; }

    }
}
