using Calendar.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using _ed = Calendar.Data.EventData;
using _fd = Calendar.Data.FormatData;
using _rs = Calendar.Properties.Resources;

namespace Calendar
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var name = _rs.User1; // Alan
            // var name = _rs.User2; // David

            // Get the start and end dates of the range
            var startDate = new DateTime(2013, 06, 09);
            var endDate = new DateTime(2013, 08, 25);

            var fileName = $"{name}Calendar.json";

            string jsonFile = fileName;

            jsonFile = _ed.ReformatFile(jsonFile);
            jsonFile = _ed.ReformatDates(jsonFile);

            string jsonString = File.ReadAllText(jsonFile);

            List<EventModel>? events = JsonConvert.DeserializeObject<List<EventModel>>(jsonString);

            events = _ed.RemoveCancelEvents(events);
            _ed.SortEventData(events);

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
