using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketHub.Areas.Identity.Data;
using TicketHub.Models;
using TicketHub.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Security.Claims;

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
            var tickets = _context.Ticket.Include(t => t.Event).Include(t => t.Seller).ToList();
            return View(tickets);
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
        
            {var ticket = _context.Ticket
                .Include(t => t.Event)
                .FirstOrDefault(t => t.Id == id);

            
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order(int id)
        {
            
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ticket = _context.Ticket.Find(id);
            
            if (ticket == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                ticket.SellerId = userId;
                _context.Update(ticket);
                _context.SaveChanges();
                

                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

    }
}