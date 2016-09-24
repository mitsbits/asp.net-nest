using System;

namespace WebApplicationNest.Models
{

    public class Ticket
    {
        public Ticket()
        {
            Id = Guid.Empty;
            Priority = Priority.Low;
        }
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        private DateTimeOffset _reportedOn = DateTimeOffset.UtcNow;
        public DateTimeOffset ReportedOn { get { return _reportedOn.ToLocalTime(); } set { _reportedOn = value.ToUniversalTime(); } }
        public Priority Priority { get; set; }
    }
}
