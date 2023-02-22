using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Data
{
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
            FormatHeader();

            foreach (var item in list)
            {
                FormatEventName(item.Name);

                FormatDescription(item.Description);

                FormatStartDate(item.StartDate);

                FormatEndDate(item.EndDate);

                FormatLocation(item.Location);

                FormatHtmlLink(item.HtmlLink);
                FormatDivider();
            }

            FormatFooter();

            var newFile = "Itinerary.html";
            File.WriteAllLines(Environment.CurrentDirectory + "\\" + newFile, Content, Encoding.UTF8);
        }

        private static void FormatEventName(string name)
        {
            string line = $"<div class=\"event-name\">\n<h2>{name}</h2>\n</div>";
            Content.Add(line);
        }

        private static void FormatHtmlLink(string htmlLink)
        {
            var link = $"<p><strong>link:</strong> <a href=\"{htmlLink}\">Link to calendar event.</a></p>\n";
            Content.Add(link);
        }

        private static void FormatLocation(string location)
        {
            var day = $"<p><strong>Location:</strong> {location}</p>\n";
            Content.Add(day);
        }

        private static void FormatEndDate(DateTime endDate)
        {
            var day = $"<p><strong>End date:</strong> {endDate}</p>\n";
            Content.Add(day);
        }

        private static void FormatStartDate(DateTime startDate)
        {
            var day = $"<p><strong>Start date:</strong> {startDate}</p>\n";
            Content.Add(day);
        }

        private static void FormatDescription(string description)
        {
            var divStart = "<div=\"description\">\n";
            Content.Add(divStart);
            var label = "<p><strong>Description: </strong></p>\n";
            Content.Add(label);

            var desc = $"<p>{description}</p>\n";
            Content.Add(desc);

            var divEnd = "</div>\n";
            Content.Add(divEnd);
        }

        private static void FormatHeader()
        {
            string line = string.Empty;

            line = "<!DOCTYPE html>\n";
            Content.Add(line);
            line = "<html lang=\"en\">\n";
            Content.Add(line);
            line = "<head>\n";
            Content.Add(line);
            line = "<meta charset=\"UTF-8\">\n";
            Content.Add(line);
            line = "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n";
            Content.Add(line);
            line = "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n";
            Content.Add(line);
            line = "<title>Alan &amp; Jen's Itinerary</title>\n";
            Content.Add(line);
            line = "</head>\n";
            Content.Add(line);
            line = "<body>\n";
            Content.Add(line);
            line = "<h1 class=\"title\">David's Itinerary</h1>\n";
            Content.Add(line);
            line = "<div=\"container\">";
            Content.Add(line);
        }

        private static void FormatFooter()
        {
            string line = string.Empty;
            line = "</div>";
            Content.Add(line);
            line = "</body>\n";
            Content.Add(line);
            line = "</html>\n";
            Content.Add(line);
        }

        private static void FormatDivider()
        {
            var line = "<div class=\"divider\"><hr/></div>\n";
            Content.Add(line);
        }
    }
}
