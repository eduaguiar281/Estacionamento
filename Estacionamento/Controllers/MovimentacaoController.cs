using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.Entities;
using Estacionamento.Entities.Base;
using Estacionamento.Exceptions;
using Estacionamento.Factories;
using Estacionamento.Services;
using Estacionamento.ViewModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Estacionamento.Controllers
{
    public class MovimentacaoController : Controller
    {
        private readonly IMovimentacaoVeiculoViewModelFactory _factory;
        private readonly IVeiculoServices _veiculoService;
        private readonly IMovimentacaoService _movimentacaoService;
        private readonly IValidator<Veiculo> _validator;
        public MovimentacaoController(IMovimentacaoVeiculoViewModelFactory factory, IVeiculoServices veiculoServices, IValidator<Veiculo> validation, IMovimentacaoService movimentacaoService)
        {
            _factory = factory;
            _veiculoService = veiculoServices;
            _validator = validation;
            _movimentacaoService = movimentacaoService;
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

        public IActionResult Saida(int id)
        {
            var model = _factory.PrepareSaidaVeiculoViewModel(id);
            return View(model);
        }

        public IActionResult CalculaHora(int id, DateTime dataSaida)
        {
            var result = _movimentacaoService.CalculaPermanencia(id, dataSaida);
            //Json.Serialize()
            SaidaVeiculoViewModel vm =  _factory.PrepareSaidaVeiculoViewModel(result);
            return Ok(JsonConvert.SerializeObject(vm));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarSaida(SaidaVeiculoViewModel viewModel)
        {
            try
            {
                
                await _factory.SaveSaidaAsync(viewModel.Id, viewModel.Saida);
            }
            catch (ModelValidateException ex)
            {
                ex.Results.AddToModelState(ModelState, null);
                return View("Saida", viewModel);
            }
            catch (Exception ex)
            {
                viewModel.Mensagem = $"Ocorreu um erro inesperado! Descricão: {ex.Message}";
                return View("Saida", viewModel);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEntrada(EntradaVeiculoViewModel viewModel)
        {
            viewModel.Mensagem = string.Empty;
            try
            {
                await _factory.SaveEntradaAsync(viewModel);
            }
            catch (ModelValidateException ex)
            {
                ex.Results.AddToModelState(ModelState, null);
                return View("Registrar", viewModel);
            }
            catch (Exception ex)
            {
                viewModel.Mensagem = $"Ocorreu um erro inesperado! Descricão: {ex.Message}";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EntityBase movId)
        {
            if (movId == null)
            {
                return BadRequest($"Parametro {nameof(movId)} não foi informado!");
            }

            var model = await _movimentacaoService.GetQuery().Where(t => t.Id == movId.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound("Não foi encontrado a movimentação informada!");
            }
            await _movimentacaoService.DeleteAsync(model);
            return Ok();
        }
    }
}