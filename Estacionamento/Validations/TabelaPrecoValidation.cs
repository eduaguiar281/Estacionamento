using Estacionamento.Core;
using Estacionamento.Entities;
using Estacionamento.Repositories;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace Estacionamento.Validations
{
    public class TabelaPrecoValidation: BaseValidation<TabelaPreco>
    {
        public TabelaPrecoValidation(IRepository<TabelaPreco> tabelaPrecoService)
        {
            RuleFor(v => v).Custom((t, context) => 
            {
                if (t.Inicio > t.Fim)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.Inicio), "Data de Início não pode ser maior que a data final!"));
                    context.AddFailure(new ValidationFailure(nameof(t.Fim), "Data de Início não pode ser maior que a data final!"));
                }
                var tab = tabelaPrecoService.Table.Where(w => t.Inicio >= w.Inicio && t.Inicio <= w.Fim && w.Id != t.Id).FirstOrDefault();
                if (tab != null)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.Inicio), $"Data de Início coincide com a vigência da tabela de preço {tab.Id}!"));
                }

                tab = null;
                tab = tabelaPrecoService.Table.Where(w => t.Fim >= w.Inicio && t.Fim <= w.Fim && w.Id != t.Id).FirstOrDefault();
                if (tab != null)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.Fim), $"Data de Fim coincide com a vigência da tabela de preço {tab.Id}!"));
                }

                if (t.Preco <= 0)
                    context.AddFailure(new ValidationFailure(nameof(t.Preco), "Preço não pode ser menor ou igual a zero!"));
                if (t.ToleranciaMinutos <= 0)
                    context.AddFailure(new ValidationFailure(nameof(t.ToleranciaMinutos), "Tolerância não pode ser menor ou igual a zero!"));
            });
        }
    }
}
