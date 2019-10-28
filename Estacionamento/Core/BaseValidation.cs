using Estacionamento.DataBase.DataContext;
using Estacionamento.Entities.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Estacionamento.Core
{
    public class BaseValidation<TModel>: AbstractValidator<TModel> where TModel : class
    {
        public BaseValidation()
        {

        }

    }
}
