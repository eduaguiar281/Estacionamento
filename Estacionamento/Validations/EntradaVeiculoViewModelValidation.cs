using Estacionamento.Core;
using Estacionamento.Services;
using Estacionamento.ViewModel;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Validations
{
    public class EntradaVeiculoViewModelValidation : BaseValidation<EntradaVeiculoViewModel>
    {
        public EntradaVeiculoViewModelValidation(ITabelaPrecosService tabelaPrecosService, IVeiculoServices veiculoServices)
        {
            RuleFor(v => v).Custom((t, context) =>
            {
                var tab = tabelaPrecosService.GetQuery().Where(w => t.DataEntrada >= w.Inicio && t.DataEntrada <= w.Fim).FirstOrDefault();
                if (tab == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.DataEntrada), "Não existe tabela de preços vigente para a Data de Entrada informada!"));
                }

                var veiculo = veiculoServices.GetQuery().Where(v => v.Id == t.VeiculoId).FirstOrDefault();
                if (veiculo == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(t.VeiculoId), $"Não foi encontrado veículo {t.VeiculoId} informado na inclusão!"));
                }
            });

        }
    }
}
