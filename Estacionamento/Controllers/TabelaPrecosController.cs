using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.Entities;
using Estacionamento.Entities.Base;
using Estacionamento.Exceptions;
using Estacionamento.Factories;
using Estacionamento.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Controllers
{
    public class TabelaPrecosController : Controller
    {
        private readonly ITabelaPrecoViewModelFactory _tabelaPrecoViewModelFactory;
        private readonly ITabelaPrecosService _tabelaPrecoService;
        public TabelaPrecosController(ITabelaPrecoViewModelFactory tabelaPrecoViewModelFactory, ITabelaPrecosService tabelaPrecosService)
        {
            _tabelaPrecoViewModelFactory = tabelaPrecoViewModelFactory;
            _tabelaPrecoService = tabelaPrecosService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _tabelaPrecoViewModelFactory.PrepareViewModelAsync();
            return View(model);
        }

        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            var model = await _tabelaPrecoViewModelFactory.PrepareToCreateAsync();
            return View("Edit", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _tabelaPrecoService.GetQuery().Where(t => t.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TabelaPreco tabela)
        {
            try
            {
                if (tabela.Id == 0)
                    await _tabelaPrecoService.CreateAsync(tabela);
                else
                    await _tabelaPrecoService.UpdateAsync(tabela);
            }
            catch(ModelValidateException ex)
            {
                ex.Results.AddToModelState(ModelState, null);
                return View("Edit", tabela);
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EntityBase tabela)
        {
            if (tabela == null)
            {
                return BadRequest("Parametro tabela não foi informado!");
            }

            var model = await _tabelaPrecoService.GetQuery().Where(t => t.Id == tabela.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound("Não foi encontrado a tabela de preço informada!");
            }
            await _tabelaPrecoService.DeleteAsync(model);
            return Ok();
        }

    }
}