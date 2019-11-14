using Estacionamento.Core;
using Estacionamento.ViewModel;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Validations
{
    public class SaidaVeiculoViewModelValidation: BaseValidation<SaidaVeiculoViewModel>
    {
        public SaidaVeiculoViewModelValidation()
        {
            RuleFor(v => v).Custom((t, context) =>
            {
                if (t.Saida < t.Entrada)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.Saida), "Saída não pode ser menor que a entrada!"));
                }

                if (t.Quantidade <=  0)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.Quantidade), "Quantidade não pode ser menor ou igual a zero!"));
                }

                if (t.ValorHora <= 0)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.ValorHora), "R$ Hora não pode ser menor ou igual a zero!"));
                }
            });
        }
    }
}
