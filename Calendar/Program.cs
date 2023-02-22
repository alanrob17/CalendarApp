using Calendar.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using _ed = Calendar.Data.EventData;
using _fd = Calendar.Data.FormatData;

namespace Calendar
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var fileName = "DavidCalendar.json";

            string jsonFile = fileName;

            jsonFile = _ed.ReformatFile(jsonFile);
            jsonFile = _ed.ReformatDates(jsonFile);

            string jsonString = File.ReadAllText(jsonFile);

            List<EventModel>? events = JsonConvert.DeserializeObject<List<EventModel>>(jsonString);

            events = _ed.RemoveCancelEvents(events);
            _ed.SortEventData(events);
            
            // Get the start and end dates of the range
            var startDate = new DateTime(2023, 08, 01);
            var endDate = new DateTime(2023, 10, 31);

            List<ShortEventModel> list = _ed.CreateSelectedEventsList(events, startDate, endDate);

            _fd.CreateHtmlFile(list);
            
            //foreach (var ev in list)
            //{
            //    Console.WriteLine($"{ev.EventId}: {ev.Name}\n\n\t{ev.Description}\n\t{ev.StartDate}\n\t{ev.EndDate}\n\t{ev.Location}\n\t{ev.HtmlLink}\n\n");
            //}

            Console.WriteLine("Finished...");
        }
    }
}
