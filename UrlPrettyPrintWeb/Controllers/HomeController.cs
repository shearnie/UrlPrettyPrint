using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace UrlPrettyPrintWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string txturl)
        {
            ViewBag.q = txturl;
            ViewBag.pretty = UrlPrettyPrint.Pretty.Print(txturl);
            return View("Pretty");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}