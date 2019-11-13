using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.ViewModel
{
    public class ListaMovimentacaoViewModel
    {
        public ListaMovimentacaoViewModel()
        {
            DataInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            PageSize = 20;
            PageNumber = 1;
        }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public IList<MovimentacaoViewModel> Movimentacoes { get; set; }
    }
}
