using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Exceptions
{
    public class ModelValidateException: Exception
    {
        public ValidationResult Results { get; private set; }

        public ModelValidateException(string message, ValidationResult results)
            :base(message)
        {
            Results = results;
        }

        public ModelValidateException(string message, Exception innerException, ValidationResult results)
            :base(message, innerException)
        {
            Results = results;
        }
    }
}
