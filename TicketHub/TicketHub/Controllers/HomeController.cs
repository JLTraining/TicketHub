using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Security.Claims;
using TicketHub.Areas.Identity.Data;
using TicketHub.Models;
using TicketHub.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Sockets;

namespace TicketHub.Controllers
{
	public class HomeController : Controller
	{
        private readonly ApplicationDbContext _context;

		public HomeController( ApplicationDbContext context)
		{

			_context = context;
		}

		public IActionResult Index()
        {


            // Lai atlasitu visus ticketus no visiem useriem			
            var applicationDbContext = _context.Ticket.Include(t => t.Event).Include(t => t.Seller);
            return View(applicationDbContext.ToList());

		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        //Buy ticket
        // GET: Tickets/Edit/5
        public IActionResult Order(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = _context.Ticket.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", ticket.EventId);
            ViewData["SellerId"] = new SelectList(_context.User, "Id", "FirstName", ticket.SellerId);
            return View("~/Views/Tickets/Index.cshtml", ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order(int id, [Bind("Id,EventId,BuyerId,Price,Quantity, Row, Seat")] OrderTicket orderTicket)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = _context.Ticket.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                    ticket.SellerId = orderTicket.BuyerId;
                    ticket.EventId = orderTicket.EventId;
                    ticket.Price = orderTicket.Price;
                    ticket.Quantity = orderTicket.Quantity;
                    ticket.Row = orderTicket.Row;
                    ticket.Seat = orderTicket.Seat;

                    _context.Update(ticket);
                    _context.SaveChanges();
                
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", ticket.EventId);
            ViewData["SellerId"] = new SelectList(_context.User, "Id", "FirstName", ticket.SellerId);
            return View(ticket);
        }

    }
}