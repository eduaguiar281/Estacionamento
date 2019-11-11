using Estacionamento.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Entities
{
    
    public class TabelaPreco: EntityBase
    {
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public decimal Preco { get; set; }
        public int ToleranciaMinutos { get; set; }

    }
}
