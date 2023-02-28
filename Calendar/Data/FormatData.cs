using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using _rs = Calendar.Properties.Resources;

namespace Calendar.Data
{
    // TODO: If the StartDate and endDate are the same then don't diplay the endDate. This also works for only 1 day difference.
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

            BuildHeader(header);

            foreach (var item in list)
            {
                if (item.StartDate.Month > monthStamp)
                {
                    monthStamp = item.StartDate.Month;

                    BuildMonthDiv(item.StartDate);
                }

                BuildCard(item);
            }

            var newFile = "Itinerary.html";
            File.WriteAllLines(Environment.CurrentDirectory + "\\" + newFile, Content, Encoding.UTF8);
        }

        private static void BuildHeader(string header)
        {
            Content.Add("<div class=\"text-center\">");
            Content.Add($"<h1 id=\"itinerary-header\" class=\"title\">{header}</h1>");
            Content.Add("</div>");
        }

        private static void BuildMonthDiv(DateTime monthStamp)
        {
            string month = monthStamp.ToString("MMMM");
            Content.Add("<div class=\"row mt-5\">");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("<div class=\"col-sm-8\">");
            Content.Add($"<div><span><a href=\"#{month.ToLower()}-link\" id=\"{month.ToLower()}\"></a></span><h2 class=\"dark-blue\">{month}</h2></div>");
            Content.Add("</div>");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("</div>");
        }

        private static void BuildCard(ShortEventModel item)
        {
            var startDate = string.Empty;
            var endDate = string.Empty;
            var description = item.Description;

            Content.Add("<div class=\"row mt-3\">");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("<div class=\"col-sm-8\">");
            Content.Add("<div class=\"card\">");
            Content.Add("<div class=\"card-body\">");
            Content.Add($"<h2 class=\"card-title\">{item.Name}</h2>");

            var formattedDate = FormatDateTime(item.StartDate);

            Content.Add($"<h5 class=\"mt-0 mb-3\">{formattedDate}</h5>");

            if (!string.IsNullOrEmpty(description))
            {
                description = ReplaceNewLines(description);
            }

            Content.Add("<div class=\"description\">");
            Content.Add($"<p class=\"card-text\"><strong>Description:</strong><br/>{description}</p>");
            Content.Add("</div>");


            if (item.StartDate.Day != item.EndDate.Day && item.StartDate.AddDays(1).Day != item.EndDate.Day)
            {
                startDate = FormatDateTime(item.StartDate);
                Content.Add($"<p class=\"card-text\"><strong>Start date:</strong> {startDate}</p>");
                endDate = FormatDateTime(item.EndDate);
                Content.Add($"<p class=\"card-text\"><strong>&nbsp;&nbsp;End date:</strong> {endDate}</p>");
            }
            else
            {
                startDate = FormatDateTime(item.StartDate);
                Content.Add($"<p class=\"card-text\"><strong>Date:</strong> {startDate}</p>");
            }

            var location = BuildLocation(item.Location);
            if (!string.IsNullOrEmpty(location))
            {
                Content.Add($"<p class=\"card-text\"><strong>Location:</strong><br/>&nbsp;&nbsp;&nbsp;&nbsp;{location}</p>");
            }
            Content.Add($"<p class=\"card-text\"><strong>link:</strong><br/><a href=\"{item.HtmlLink}\" target=\"_blank\">Calendar event.</a></p>");
            Content.Add("</div>");
            Content.Add("</div>");
            Content.Add("</div>");
            Content.Add("<div class=\"col-sm-2\"></div>");
            Content.Add("</div>");
        }

        private static string BuildLocation(string location)
        {
            var googleLocation = string.Empty;

            if (!string.IsNullOrEmpty(location))
            {
                googleLocation = location.Replace(' ', '+');
                location = $"<a href=\"https://google.com/maps?q={googleLocation}\" target=\"_blank\">{location}</a>";
                return location;
            }
            return string.Empty;
        }

        private static string FormatDateTime(DateTime startDate)
        {
            var day = startDate.Day;
            var suffix = day.OrdinalSuffix();
            var formattedDateTime = $"{day}{day.OrdinalSuffix()} {startDate.ToString("MMMM yyyy - h:mm tt")}";

            return formattedDateTime;
        }

        private static string ReplaceNewLines(string description)
        {
            if (description.Contains("\n"))
            {
                description = description.Replace("\n", "<br/>");
            }

            return description;
        }

        // not being used.
        private static string CreateHrefs(string description)
        {
            Regex urlRegex = new Regex(@"(https?://)(\S+)");
            description = urlRegex.Replace(description, "<a href=\"$1$2\" target=\"_blank\">" + urlRegex.Replace("$2", "", 1) + "</a>");

            return description;
        }
    }
}
