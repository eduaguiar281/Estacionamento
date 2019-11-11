using Estacionamento.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.ViewModel
{
    public class TabelaPrecosViewModel
    {
        public TabelaPrecosViewModel()
        {

        }

        public IList<TabelaPreco> TabelaPrecos { get; set; }
    }
}
