using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace UrlPrettyPrint.Controllers
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

            if (string.IsNullOrEmpty(txturl)) return View("Pretty");

            var sb = new StringBuilder();

            // split up
            var qs = txturl.Split('?');
            sb.AppendLine(qs[0]);
            sb.AppendLine();

            // get params
            var parms = qs.Count() < 2 ? qs[0] : qs[1];
            var parse = HttpUtility.ParseQueryString(parms);
            var keys = (from string k in parse.Keys where !string.IsNullOrEmpty(k) select k).ToList();

            if (!keys.Any()) return View("Pretty");

            // determine spacing
            var longestLength = keys.OrderByDescending(k => k.Length).First().Length;

            foreach (string k in keys)
            {
                sb.Append(k);
                sb.Append(new String(' ', longestLength - k.Length));
                sb.Append(" = ");
                sb.AppendLine(parse[k]);
            }

            ViewBag.pretty = sb.ToString();
            return View("Pretty");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}