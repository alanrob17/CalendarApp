using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using _rs = Calendar.Properties.Resources;

namespace Calendar.Data
{
    // TODO: description has line breaks - replace these with Paragraph breaks.
    // TODO: Work out the first time we change from one month to the next. Put a position value on that event e.g. #august. Use for positioning
    // TODO: If the StartDate and endDate are the same then don't diplay the endDate. This also works of only 1 day difference.
    internal class FormatData
    {
        public static List<string> Content = new();

        // Create an HTML file from the event data.
        internal static void CreateHtmlFile(List<ShortEventModel> list)
        {
            if (list != null)
            {
                ReformatEventsToHtml(list);
            }

        }

        private static void ReformatEventsToHtml(List<ShortEventModel> list)
        {
            var header = _rs.AlanHeader;
            // var header = _rs.DavidHeader;

            int monthStamp = 0;

            FormatHeader(header);

            foreach (var item in list)
            {
                if(item.StartDate.Month > monthStamp)
                {
                    monthStamp = item.StartDate.Month;

                    BuildMonthDiv(item.StartDate);

            
                }

                BuildCard(item);
            }

            FormatFooter();

            var newFile = "Itinerary.html";
            File.WriteAllLines(Environment.CurrentDirectory + "\\" + newFile, Content, Encoding.UTF8);
        }

        private static void BuildMonthDiv(DateTime monthStamp)
        {
            string month = monthStamp.ToString("MMMM");
            Content.Add("<div class=\"row mt-3\">");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("<div class=\"col-sm-8\">");
            Content.Add($"<div><span><a href=\"#{month.ToLower()}\" id=\"{month.ToLower()}-link\"></a></span><br/><br/><br/><h1 class=\"dark-blue\">{month}</h1></div>");
            Content.Add("</div>");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("</div>");
        }

        private static void BuildCard(ShortEventModel item)
        {
            var description = string.Empty;

            if (description != null && description.Contains("\n"))
            {
                description = item.Description.Replace("\n", "<br/>");
            }
            
            Content.Add("<div class=\"row mt-3\">");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("<div class=\"col-sm-8\">");
            Content.Add("<div class=\"card\">");
            Content.Add("<div class=\"card-body\">");
            Content.Add($"<h2 class=\"card-title\">{item.Name}</h2>");
            Content.Add("<div class=\"description\">");
            Content.Add($"<p class=\"card-text\"><strong>Description:</strong><br/>{description}</p>");
            Content.Add("</div>");
            Content.Add($"<p class=\"card-text\"><strong>Start date:</strong> {item.StartDate}</p>");
            Content.Add($"<p class=\"card-text\"><strong>End date:</strong> {item.EndDate}</p>");
            Content.Add($"<p class=\"card-text\"><strong>Location:</strong><br/>{item.Location}</p>");
            Content.Add($"<p class=\"card-text\"><strong>link:</strong><br/><a href=\"{item.HtmlLink}\">Calendar event.</a></p>");
            Content.Add("</div>");
            Content.Add("</div>");
            Content.Add("</div>");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("</div>");
        }

        private static void FormatHeader(string header)
        {
            Content.Add("<!DOCTYPE html>");
            Content.Add("<html lang=\"en\">");
            Content.Add("<head>");
            Content.Add("<meta charset=\"UTF-8\">");
            Content.Add("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
            Content.Add("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            Content.Add($"<title>{header}</title>");
            Content.Add("</head>");
            Content.Add("<body>");
            Content.Add("<div class=\"row mt-3\">");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("<div class=\"col-sm-8\">");
            Content.Add("<div class=\"text-center mt-5\">");
            Content.Add($"<h1 class=\"title\">{header}</h1>");
            Content.Add("</div>");
            Content.Add("</div>");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("</div>");
        }

        private static void FormatFooter()
        {
            Content.Add("</body>\n</html>");
        }
    }
}
