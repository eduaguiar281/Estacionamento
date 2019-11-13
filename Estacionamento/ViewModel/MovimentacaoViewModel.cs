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
        private readonly Movimentacao _currentMovimentacao;
        private bool _exibicao;
        public MovimentacaoViewModel(Movimentacao movimentacao, bool exibicao)
        {
            _currentMovimentacao = movimentacao;
            _exibicao = exibicao;
            Id = movimentacao.Id;
            Placa = movimentacao.Veiculo?.Placa;
            Descricao = movimentacao.Veiculo?.Descricao;
            DataEntrada = movimentacao.Entrada;
            DataSaida = movimentacao.Saida;
            Preco = movimentacao.Valor;
            Quantidade = movimentacao.Quantidade;
            ValorTotal = movimentacao.ValorTotal;
            CalculePermanencia();
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

        private void CalculePermanencia()
        {
            if (!DataSaida.HasValue)
                return;
            Duracao = (DataSaida.Value - DataEntrada);

            if (!_exibicao)
                return;

            var tabela = _currentMovimentacao.TabelaPreco;
            double totalMinutos = (DataSaida.Value - DataEntrada).TotalMinutes;
            int qtHoras = Convert.ToInt32(Math.Round(totalMinutos / 60, 0));
            var resto = totalMinutos % 60;
            if (tabela.ToleranciaMinutos < resto)
                qtHoras++;
            ValorTotal = tabela.Preco * qtHoras;
        }
    }
}
