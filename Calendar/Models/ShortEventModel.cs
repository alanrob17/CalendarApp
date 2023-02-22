using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class ShortEventModel
    {
        public int EventId { get; set; }
        public string Name { get; set; } 
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string HtmlLink { get; set; }

        public ShortEventModel(int eventId, string name, string location, DateTime startDate, DateTime endDate, string description, string htmlLink) 
        {
            this.EventId = eventId;
            this.Name = name;
            this.Location = location;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Description = description;
            this.HtmlLink = htmlLink;
        }
    }
}
