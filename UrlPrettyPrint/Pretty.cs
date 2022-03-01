using System.Text;
using System.Web;

namespace UrlPrettyPrint
{
    public static class Pretty
    {
        public static string Print(string urlValue)
        {

            if (string.IsNullOrWhiteSpace(urlValue))
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            // split up
            var qs = urlValue.Split('?');
            sb.AppendLine(qs[0]);
            sb.AppendLine();

            // get params
            var parms = qs.Count() < 2 ? qs[0] : qs[1];
            var parse = HttpUtility.ParseQueryString(parms);
            var keys = (from string k in parse.Keys where !string.IsNullOrEmpty(k) select k).ToList();

            if (!keys.Any())
            {
                return string.Empty;
            }

            // determine spacing
            var longestLength = keys.OrderByDescending(k => k.Length).First().Length;

            foreach (string k in keys)
            {
                sb.Append(k);
                sb.Append(new String(' ', longestLength - k.Length));
                sb.Append(" = ");
                sb.AppendLine(parse[k]);
            }

            return sb.ToString();
        }
    }
}