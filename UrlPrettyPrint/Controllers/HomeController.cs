using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UrlPrettyPrint.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string txturl)
        {
            if (string.IsNullOrEmpty(txturl)) return View("Pretty");

            var sb = new StringBuilder();

            var qs = txturl.Split('?');
            sb.Append(qs[0]);
            sb.AppendLine("<br />");

            var parms = qs.Count() < 2 ? qs[0] : qs[1];

            var parse = HttpUtility.ParseQueryString(parms);
            foreach (string k in parse.Keys)
            {
                if (string.IsNullOrEmpty(k)) continue;
                sb.Append(k);
                sb.Append(" = ");
                sb.Append(parse[k]);
                sb.AppendLine("<br />");
            }

            ViewBag.q = txturl;
            ViewBag.pretty = sb.ToString();
            return View("Pretty");
        }
    }
}