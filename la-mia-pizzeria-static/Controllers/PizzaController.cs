﻿using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly PizzeriaContext _context;

        public PizzaController(ILogger<PizzaController> logger, PizzeriaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var pizzas = _context.Pizzas.ToArray();

            return View(pizzas);
            
        }

        public IActionResult Detail(int id)
        {
            var pizza = _context.Pizzas.SingleOrDefault(p => p.Id == id);

            if (pizza is null)
            {
                return NotFound($"Pizza numero: {id} non trovata");
            }

            return View(pizza);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View(pizza);
            }

            _context.Pizzas.Add(pizza);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var pizza = _context.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizza is null)
            {
                return View("NotFound");
            }

            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View(pizza);
            }

            var PizzaUpdate = _context.Pizzas.FirstOrDefault(p => p.Id == pizza.Id);

            if (PizzaUpdate is null)
            {
                return View("NotFound");
            }

            PizzaUpdate.Nome = pizza.Nome;
            PizzaUpdate.Descrizione = pizza.Descrizione;
            PizzaUpdate.Prezzo = pizza.Prezzo;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var EliminaPizza = _context.Pizzas.FirstOrDefault(p => p.Id == id);

            if (EliminaPizza is null)
            {
                return View("NotFound");
            }

            _context.Pizzas.Remove(EliminaPizza);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
