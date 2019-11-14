using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.Entities;
using Estacionamento.Exceptions;
using Estacionamento.Factories;
using Estacionamento.Services;
using Estacionamento.ViewModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Controllers
{
    public class MovimentacaoController : Controller
    {
        private readonly IMovimentacaoVeiculoViewModelFactory _factory;
        private readonly IVeiculoServices _veiculoService;
        private readonly IValidator<Veiculo> _validator;
        public MovimentacaoController(IMovimentacaoVeiculoViewModelFactory factory, IVeiculoServices veiculoServices, IValidator<Veiculo> validation)
        {
            _factory = factory;
            _veiculoService = veiculoServices;
            _validator = validation;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _factory.CreateListaMovimentacaoViewModelAsync();
            return View(model);
        }

        public IActionResult Registrar()
        {
            var model = _factory.CreateEntradaViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEntrada(EntradaVeiculoViewModel viewModel)
        {
            try
            {
                await _factory.SaveEntradaAsync(viewModel);
            }
            catch (ModelValidateException ex)
            {
                ex.Results.AddToModelState(ModelState, null);
                return View("Registrar", viewModel);
            }
            return RedirectToAction("Index");
        }

        private string GetMessages(IList<ValidationFailure> list)
        {
            string mensagem = string.Empty;
            foreach (var err in list)
                mensagem += $"{err.ErrorMessage} <br>";
            return mensagem;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetVeiculo([FromBody] Veiculo veiculo)
        {
            if (veiculo == null)
                throw new ArgumentNullException(nameof(veiculo));

            var veiculoDb = _veiculoService.GetQuery().Where(v => v.Placa == veiculo.Placa).FirstOrDefault();
            if (veiculoDb == null)
            {
                var validates = _validator.Validate(veiculo);
                if (!validates.IsValid)
                {
                    return BadRequest(GetMessages(validates.Errors));                   
                }
                if (string.IsNullOrEmpty(veiculo.Descricao))
                    return Ok();
                try
                {
                    await _veiculoService.CreateAsync(veiculo);
                    return Ok(veiculo);
                }
                catch (ModelValidateException ex)
                {
                    return BadRequest(ex.Results.Errors);
                }
            }
            return Ok(veiculoDb);
        }
    }
}