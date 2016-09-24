using Microsoft.AspNetCore.Mvc;
using System;
using WebApplicationNest.Models;

namespace WebApplicationNest.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IEntityService<Ticket, Guid> _tickets;

        public TicketsController(IEntityService<Ticket, Guid> tickets)
        {
            _tickets = tickets;
        }

        public IActionResult Index(string q)
        {
            return View(string.IsNullOrWhiteSpace(q) ? _tickets.Find() : _tickets.Find(q));
        }

        public IActionResult Details(string id)
        {
            var entity = (string.IsNullOrWhiteSpace(id)) ? new Ticket() : _tickets.Get(Guid.Parse(id));
            return View(entity);
        }

        [HttpPost]
        public IActionResult Post(Ticket entity)
        {
            var model = _tickets.Post(entity);
            return RedirectToAction("details", new { id = model.Id });
        }

        public IActionResult Delete(string id)
        {
            _tickets.Delete(Guid.Parse(id));
            return RedirectToAction("index");
        }
    }
}