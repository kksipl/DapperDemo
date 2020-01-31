using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperDemo.Models;
using DapperDemo.DAL.Repository;

namespace DapperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITenant _tenantRepository;

        public HomeController(ITenant tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public IActionResult Index()
        {
            var tenants = _tenantRepository.GetAllTenant();
            return View(tenants);
        }

        public ActionResult Details(int id)
        {
            var tenant = _tenantRepository.GetTenantById(id);
            return View(tenant);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
