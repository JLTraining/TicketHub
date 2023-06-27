using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketHub.Areas.Identity.Data;
using TicketHub.DataTransferObjects;
using TicketHub.Models;

namespace TicketHub.Controllers
{
    
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Lai atlasitu visus ticketus no visiem useriem
            // var applicationDbContext = _context.Ticket.Include(t => t.Event).Include(t => t.Seller);

            // Lai atlasitu tikai savus ticketus
            var applicationDbContext = _context.Ticket.Include(t => t.Event).Include(t => t.Seller).Where(e => e.SellerId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Event)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new CreateTicket();
            // Query for only the Events that belong to the logged-in User
            var users = _context.User.Where(e => e.Id == userId);

            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title");
            //ViewData["SellerId"] = new SelectList(users, "Id", "FirstName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,EventId,SellerId,Price,Quantity,Row,Seat")] CreateTicket createTicket)
        {
            /** Visur kur vajag, lai dabutu userId. **/
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ticket = new Ticket();
            //Check for duplicate Row and seat combinations in events with same title or id
            var eventTitle = _context.Event.Where(e => e.Id == createTicket.EventId).Select(e => e.Title).FirstOrDefault();
           // var existingTitle = _context.Event.FirstOrDefault(t => t.Title == eventTitle);
            var existingTicket = _context.Ticket.FirstOrDefault(t => t.EventId == createTicket.EventId && t.Row == createTicket.Row && t.Seat == createTicket.Seat);

            if (existingTicket != null )
            {
                ModelState.AddModelError("Seat", "A ticket with the same Row and Seat already exists.");
            }
            if (ModelState.IsValid)
            {
                    ticket.SellerId = userId;
                    ticket.EventId = createTicket.EventId;
                    ticket.Price = createTicket.Price;
                    ticket.Quantity = createTicket.Quantity;
                if (createTicket.Quantity == 1)
                {
                    ticket.Row = createTicket.Row;
                    ticket.Seat = createTicket.Seat;
                }

                _context.Add(ticket);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
             }

             ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", createTicket.EventId);
             //ViewData["SellerId"] = new SelectList(_context.User, "Id", "FirstName", createTicket.SellerId);

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", ticket.EventId);
            ViewData["SellerId"] = new SelectList(_context.User, "Id", "FirstName", ticket.SellerId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,SellerId,Price,Quantity, Row, Seat")] EditTicket editTicket)
        {
            //Pārbauda vai datubāzē vispār ir kaut viena biļete
            if (_context.Ticket == null)
            {
                return NotFound();
            }

            //inicializē mainīgo', kas glabā konkrētas biļetes identifikatoru
            var ticket = _context.Ticket.Find(id);
            //Ja šis identifikators netika atrasts ar _context.Ticket.Find(id), ticket ir bez vērtības
            if (ticket == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    ticket.SellerId = editTicket.SellerId;
                    ticket.EventId = editTicket.EventId;
                    ticket.Price = editTicket.Price;
                    ticket.Quantity = editTicket.Quantity;
                    ticket.Row = editTicket.Row;
                    ticket.Seat = editTicket.Seat;

                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", ticket.EventId);
            ViewData["SellerId"] = new SelectList(_context.User, "Id", "FirstName", ticket.SellerId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ticket == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Event)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ticket'  is null.");
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        //List ticket
        // GET: Tickets/List/5
        public IActionResult List(int? id)

        {
            var ticket = _context.Ticket
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult List(int id)
        {
            var ticket  = _context.Ticket.Find(id);

            if (ticket == null) 
            { 
                return NotFound(); 
            }

            else if(ModelState.IsValid)
            {   if (ticket.isListed == false)
                {
                    ticket.isListed = true;
                    _context.Update(ticket);
                    _context.SaveChanges();
                }
                else if (ticket.isListed == true)
                {
                    ticket.isListed = false;
                    _context.Update(ticket);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }
    }
}
