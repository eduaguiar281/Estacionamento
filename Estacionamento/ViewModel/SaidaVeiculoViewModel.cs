using Estacionamento.Entities;
using Estacionamento.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Estacionamento.ViewModel
{
    [DataContract]
    public class SaidaVeiculoViewModel
    {
        public SaidaVeiculoViewModel()
        {
            
        }

        [Display(Name = "Id")]
        [DataMember]
        public int Id { get; set; }

        [Display(Name = "Entrada")]
        [DataMember]
        public DateTime Entrada { get; set; }

        [Display(Name = "Veiculo")]
        [DataMember]
        public string Veiculo { get; set; }

        [Display(Name = "Saída")]
        [DataMember]
        public DateTime Saida { get; set; }

        [Display(Name = "Permanência")]
        [DataMember]
        public string Permanencia { get; set; }

        [Display(Name = "Qtd.")]
        [DataMember]
        public int Quantidade { get; set; }

        [Display(Name = "R$ Hora")]
        [DataMember]
        public decimal ValorHora { get; set; }

        [Display(Name = "Total")]
        [DataMember]
        public decimal Total { get; set; }

        [Display(Name = "Mensagem")]
        [DataMember]
        public string Mensagem { get; set; }
    }
}
