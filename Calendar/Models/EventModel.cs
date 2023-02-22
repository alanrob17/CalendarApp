using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Models
{
    internal class EventModel
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string HtmlLink { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public CreatorModel Creator { get; set; }
        public OrganizerModel Organizer { get; set; }
        public StartModel Start { get; set; }
        public EndModel End { get; set; }
        public string Transparency { get; set; }
        public string ICalUID { get; set; }
        public int Sequence { get; set; }
        public RemindersModel Reminders { get; set; }
        public List<AttachmentModel> Attachments { get; set; }
        public string EventType { get; set; }
    }
}
