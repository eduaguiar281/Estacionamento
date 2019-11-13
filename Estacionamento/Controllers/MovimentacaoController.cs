using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estacionamento.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.Controllers
{
    public class MovimentacaoController : Controller
    {
        private readonly IMovimentacaoVeiculoViewModelFactory _factory;
        public MovimentacaoController(IMovimentacaoVeiculoViewModelFactory factory)
        {
            _factory = factory;
        }
        public IActionResult Index()
        {
            var model = _factory.CreateListaMovimentacaoViewModelAsync();
            return View(model);
        }
    }
}