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

        private void SetModel(Movimentacao mov)
        {
            Id = mov.Id;
            Entrada = mov.Entrada;
            Saida = mov.Saida ?? DateTime.Now;
            Veiculo = $"{mov.Veiculo.Placa}- {mov.Veiculo.Descricao}";
            Permanencia = (Saida - Entrada).ToString("hh\\:mm");
            Quantidade = mov.Quantidade ?? 0;
            ValorHora = mov.Valor ?? 0;
            Total = mov.ValorTotal ?? 0;
            Mensagem = string.Empty;
        }
        public SaidaVeiculoViewModel()
        {
            
        }

        public SaidaVeiculoViewModel(Movimentacao mov)
        {
            SetModel(mov);
        }
        public SaidaVeiculoViewModel(int idMovimentacao, IMovimentacaoService movimentacaoService)
        {
            var mov = movimentacaoService.GetQuery().Include("Veiculo").Include("TabelaPreco").Where(m => m.Id == idMovimentacao).FirstOrDefault();
            if (mov == null)
                throw new ArgumentException($"Não foi encontrado movimentação com o id {idMovimentacao}", nameof(idMovimentacao));
            if (!mov.Saida.HasValue)
            {
                Saida = DateTime.Now;
                mov.Saida = Saida;
                movimentacaoService.CalculaPermanencia(mov);
            }
            SetModel(mov);
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
