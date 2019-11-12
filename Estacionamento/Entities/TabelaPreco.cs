using Estacionamento.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Estacionamento.Entities
{
    [DataContract]
    public class TabelaPreco: EntityBase
    {
        [DataMember]
        public DateTime Inicio { get; set; }
        [DataMember]
        public DateTime Fim { get; set; }
        [DataMember]
        public decimal Preco { get; set; }
        [DataMember]
        public int ToleranciaMinutos { get; set; }

    }
}
