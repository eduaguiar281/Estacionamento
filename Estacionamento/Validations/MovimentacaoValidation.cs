using Estacionamento.Core;
using Estacionamento.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Validations
{
    public class MovimentacaoValidation: BaseValidation<Movimentacao>
    {
        public MovimentacaoValidation()
        {
            RuleFor(v => v).Custom((m, context) => 
            {

                if (m.TabelaPrecoId == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(m.TabelaPrecoId), "Tabela de Preço não foi informado!"));
                }

                if (m.VeiculoId == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(m.VeiculoId), "Veículo não foi informado!"));
                }

                if ((m.Valor ?? 0) == 0)
                {
                    context.AddFailure(new ValidationFailure(nameof(m.Valor), "Valor não da hora não foi informado!"));

                }
            });
        }
    }
}
