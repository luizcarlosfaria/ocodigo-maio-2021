using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OCodigoData;
using OCodigoWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OCodigoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataAccess dataAccess;

        public HomeController(ILogger<HomeController> logger, DataAccess dataAccess)
        {
            _logger = logger;
            this.dataAccess = dataAccess;
        }

        public IActionResult Index()
        {
            var customers = dataAccess.GetCustomers();

            return View(customers);
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
