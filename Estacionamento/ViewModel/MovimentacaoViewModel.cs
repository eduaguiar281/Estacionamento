using Estacionamento.Entities;
using Estacionamento.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.ViewModel
{
    public class MovimentacaoViewModel
    {
        public MovimentacaoViewModel(Movimentacao movimentacao)
        {
            Id = movimentacao.Id;
            Placa = movimentacao.Veiculo?.Placa;
            Descricao = movimentacao.Veiculo?.Descricao;
            DataEntrada = movimentacao.Entrada;
            DataSaida = movimentacao.Saida;
            Preco = movimentacao.Valor;
            Quantidade = movimentacao.Quantidade;
            ValorTotal = movimentacao.ValorTotal;
            CalculeDuracao();
        }

        public int Id { get; set; }
        public string Placa { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public TimeSpan? Duracao { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Preco { get; set; }
        public decimal? ValorTotal { get; private set; }

        private void CalculeDuracao()
        {
            if (!DataSaida.HasValue)
                return;
            Duracao = (DataSaida.Value - DataEntrada);
        }
    }
}
