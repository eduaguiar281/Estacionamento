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
    public class TabelaPrecoValidation: BaseValidation<TabelaPreco>
    {
        public TabelaPrecoValidation()
        {
            RuleFor(v => v).Custom((t, context) => 
            {
                if (t.Inicio > t.Fim)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.Inicio), "Data de Início não pode ser maior que a data final!"));
                    context.AddFailure(new ValidationFailure(nameof(t.Fim), "Data de Início não pode ser maior que a data final!"));
                }
                if (t.Preco <= 0)
                    context.AddFailure(new ValidationFailure(nameof(t.Preco), "Preço não pode ser menor ou igual a zero!"));
                if (t.ToleranciaMinutos <= 0)
                    context.AddFailure(new ValidationFailure(nameof(t.ToleranciaMinutos), "Tolerância não pode ser menor ou igual a zero!"));
            });
        }
    }
}
