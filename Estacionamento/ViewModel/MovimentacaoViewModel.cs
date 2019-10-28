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
        private bool _editMode;
        private readonly ITabelaPrecosService _tabelaPrecoService;
        public MovimentacaoViewModel(ITabelaPrecosService tabelaPrecosService, bool editMode)
        {
            _tabelaPrecoService = tabelaPrecosService;
            _editMode = editMode;
        }

        public DateTime DataEntrada { get; set; }

        private DateTime? _dataSaida;
        public DateTime? DataSaida 
        { 
            get
            {
                return _dataSaida;
            }
            set
            {
                _dataSaida = value;
                CalculePermanencia();
            }
        }

        private void CalculePermanencia()
        {
            if (!_editMode)
                return;
            if (!_dataSaida.HasValue)
            {
                Permanencia = 0;
            }

            var tabela = _tabelaPrecoService.GetTabelasAsync(p => p.Inicio >= DataEntrada && p.Fim <= DataEntrada).Result.FirstOrDefault();

            if (tabela == null)
                throw new ArgumentNullException("Tabela de preços não foi encontrada!");
            
            double totalMinutos = (DataSaida.Value - DataEntrada).TotalMinutes;
            int qtHoras = Convert.ToInt32(Math.Round(totalMinutos / 60, 0));
            var resto = totalMinutos % 60;
            if (tabela.ToleranciaMinutos < resto)
                qtHoras++;
            Permanencia = totalMinutos;
            ValorTotal = tabela.Preco * qtHoras;
        }

        public string Placa { get; set; }
        public string Descricao { get; set; }
        public double Permanencia { get; private set; }
        public decimal? ValorTotal { get; private set; }

    }
}
