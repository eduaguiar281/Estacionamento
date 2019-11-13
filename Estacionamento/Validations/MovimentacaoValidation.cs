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

                if (m.TabelaPreco == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(m.TabelaPreco), "Tabela de Preço não foi informado!"));
                }

                if (m.Veiculo == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(m.Veiculo), "Veículo não foi informado!"));
                }

                if ((m.Valor ?? 0) == 0)
                {
                    context.AddFailure(new ValidationFailure(nameof(m.Valor), "Valor não da hora não foi informado!"));

                }
            });
        }
    }
}
