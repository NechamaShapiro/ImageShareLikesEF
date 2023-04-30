using ImageShareLikesEF.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ImageShareLikesEF.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}