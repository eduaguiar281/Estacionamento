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

            /*
             *         
        DateTime inicio = new DateTime(2019, 10, 28, 18, 26, 35);
        DateTime final = new DateTime(2019, 10, 28, 22, 37, 45);

        TimeSpan teste = final - inicio;
        double testevalue = teste.TotalMinutes;
		Console.WriteLine(testevalue);
		
		var qtHoras = Math.Truncate(testevalue /60); 
		Console.WriteLine(qtHoras);
		var resto = testevalue % 60;
	    Console.WriteLine(resto);

             * 
             */


            TimeSpan diferenca = _dataSaida.Value - DataEntrada;
            Permanencia = diferenca.TotalMinutes;
        }

        public string Placa { get; set; }
        public string Descricao { get; set; }
        public double Permanencia { get; private set; }
        public decimal? ValorTotal { get; }

    }
}
