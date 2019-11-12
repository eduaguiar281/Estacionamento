using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Estacionamento.Entities.Base
{
    [DataContract]
    public class EntityBase
    {
        [DataMember]
        public int Id { get; set; }
    }
}
