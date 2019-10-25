using Estacionamento.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Entities
{
    public class Veiculo: EntityBase
    {
        public string Placa { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Movimentacao> Movimentacoes { get; set; }
    }
}
