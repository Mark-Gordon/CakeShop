using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CakeShop.Models;
using CakeShop.Models.Repositories;
using CakeShop.Models.Repositories.Interfaces;
using CakeShop.ViewModels;

namespace CakeShop.Controllers
{
    public class CakeController : Controller
    {
        private readonly ILogger<CakeController> _logger;
        private readonly ICakeRepository _cakeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CakeController(ILogger<CakeController> logger, ICakeRepository cakeRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _cakeRepository = cakeRepository;
        }

        public IActionResult Index()
        {
            return View(_cakeRepository.AllCakes);
        }

        public ViewResult List(string category)
        {
            IEnumerable<Cake> cakes;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                cakes = _cakeRepository.AllCakes.OrderBy(c => c.CakeId);
                currentCategory = "All cakes";
            }
            else
            {
               // System.Diagnostics.Debug.WriteLine("\n\n\n\n\n\nHERE\n\n\n\n\n");
                cakes = _cakeRepository.AllCakes.Where(c => c.Category.Name == category)
                    .OrderBy(c => c.CakeId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.Name == category)?.Name;
            }

            return View(new CakesListViewModel
            {
                Cakes = cakes,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Details(int id)
        {
            Cake cake = _cakeRepository.GetCakeById(id);

            return View(cake);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
