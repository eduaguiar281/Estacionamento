using Estacionamento.Core;
using Estacionamento.Entities;
using Estacionamento.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Estacionamento.Validations
{
    public class VeiculoValidation: BaseValidation<Veiculo>
    {
        public VeiculoValidation(IRepository<Veiculo> repository)
        {
            //Validação Placa por expressão regular
            RuleFor(x => x).Custom((veiculo, context) =>
            {
                bool modeloAntigoIsValid = Regex.IsMatch(veiculo.Placa, "[a-zA-Z]{3}[-]{1}[0-9]{4}");
                bool modeloMercosulIsValid = Regex.IsMatch(veiculo.Placa, "[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}|[A-Z]{3}[0-9]{4}");
                if (!modeloAntigoIsValid && !modeloMercosulIsValid)
                {
                    context.AddFailure(nameof(veiculo.Placa), "Placa não é válida!");
                }

                var veiculoDb = repository.Table.Where(v => v.Placa == veiculo.Placa && v.Id != veiculo.Id).FirstOrDefault();
                if (veiculoDb != null)
                {
                    context.AddFailure(nameof(veiculo.Placa), "Já existe um veículo cadastrado com esta placa!");

                }
            });
        }
    }
}
