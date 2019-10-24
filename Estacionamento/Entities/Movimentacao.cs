using Estacionamento.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Entities
{
    public class Movimentacao: EntityBase
    {
        public DateTime Entrada { get; set; }
        public DateTime? Saida { get; set; }
        public Veiculo Veiculo { get; set; }
        public int VeiculoId { get; set; }
        public decimal? Valor { get; set; }
    }
}
