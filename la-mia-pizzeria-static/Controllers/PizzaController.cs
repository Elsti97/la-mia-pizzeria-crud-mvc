using Microsoft.AspNetCore.Mvc;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
