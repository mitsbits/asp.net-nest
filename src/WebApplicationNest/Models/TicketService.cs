using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace WebApplicationNest.Models
{
    public class TicketService : IEntityService<Ticket, Guid>
    {
        private readonly IElasticClient _client;

        public TicketService(IElasticClient client)
        {
            _client = client;
        }

        public IEnumerable<Ticket> Find()
        {
            var result = _client.Search<Ticket>();
            return result.Documents.OrderByDescending(x => x.ReportedOn);
        }

        public IEnumerable<Ticket> Find(string searchterm)
        {
 
            var result = _client.Search<Ticket>(s => s
                .Query(q=> 
                    q.Term(c => c.Field(t => t.Body).Value(searchterm)) || 
                    q.Term(c => c.Field(t => t.Subject).Value(searchterm))));

            return result.Documents.OrderByDescending(x=>x.ReportedOn);
        }

        public Ticket Post(Ticket ticket)
        {
            if (ticket.Id == Guid.Empty) ticket.Id = Guid.NewGuid();
            _client.Index(ticket, i => i.Index<Ticket>().Id(ticket.Id));
            return ticket;
        }

        public Ticket Get(Guid ticketId)
        {
            var res = _client.Get<Ticket>(ticketId, i => i.Index<Ticket>());
            return (res.Found) ? res.Source : new Ticket();
        }

        public void Delete(Guid ticketId)
        {
            _client.Delete<Ticket>(ticketId);
        }
    }
}