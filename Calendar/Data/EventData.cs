using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Data
{
    internal class EventData
    {
        internal static string ReformatFile(string fileName)
        {
            var newFile = "calendar.json";
            //  
            if (File.Exists(Environment.CurrentDirectory + "\\" + fileName))
            {
                bool infile = false;
                var lines = File.ReadAllLines(Environment.CurrentDirectory + "\\" + fileName);

                List<string> newLines = new List<string>();

                for (int i = 0; i < lines.Length - 1; i++)
                {
                    if (lines[i].Contains("\"items\": ["))
                    {
                        infile = true;
                        newLines.Add("[");
                    }
                    else if (infile)
                    {
                        newLines.Add(lines[i]);
                    }
                }

                var count = lines.Length;
                lines[lines.Length - 1] = string.Empty;

                File.WriteAllLines(Environment.CurrentDirectory + "\\" + newFile, newLines, Encoding.UTF8);
            }

            return newFile;
        }

        internal static string ReformatDates(string fileName)
        {
            var newFile = "calendar.json";

            if (File.Exists(Environment.CurrentDirectory + "\\" + fileName))
            {
                var lines = File.ReadAllLines(Environment.CurrentDirectory + "\\" + fileName);

                List<string> newLines = new List<string>();

                for (int i = 0; i < lines.Length - 1; i++)
                {
                    if (lines[i].Contains("\"date\":"))
                    {
                        string changedLine = CorrectDate(lines[i]);
                        newLines.Add(changedLine);
                        newLines.Add("\t\"timeZone\": \"NONE\"");
                    }
                    else
                    {
                        newLines.Add(lines[i]);
                    }
                }

                newLines.Add("]");

                File.WriteAllLines(Environment.CurrentDirectory + "\\" + newFile, newLines, Encoding.UTF8);
            }

            return newFile;
        }

        internal static List<EventModel> RemoveCancelEvents(List<EventModel>? events)
        {
            events.RemoveAll(e => e.Status == "cancelled");

            return events;
        }

        internal static void SortEventData(List<EventModel> events)
        {
            events.Sort((e1, e2) => DateTime.Compare(e1.Start.DateTime, e2.Start.DateTime));
        }

        internal static List<ShortEventModel> CreateSelectedEventsList(List<EventModel> events, DateTime startDate, DateTime endDate)
        {
            var eventId = 0;
            List<ShortEventModel> selectedEvents = new();

            foreach (var item in events)
            {
                //
                // Check if the date is within the range
                if (DateTime.Compare(item.Start.DateTime, startDate) >= 0 && DateTime.Compare(item.Start.DateTime, endDate) <= 0)
                {
                    ShortEventModel newEvent = new ShortEventModel(++eventId, item.Summary, item.Location, item.Start.DateTime, item.End.DateTime, item.Description, item.HtmlLink);
                    selectedEvents.Add(newEvent);
                }
            }

            return selectedEvents;
        }

        internal static string CorrectDate(string line)
        {
            string day = line.Replace("\"date\": \"", string.Empty);
            day = day.Replace("\"", string.Empty);
            day = day.Trim();
            // 
            day = $"\t\"dateTime\": \"{day}T12:00:00+11:00\",";

            return day;
        }
    }
}
