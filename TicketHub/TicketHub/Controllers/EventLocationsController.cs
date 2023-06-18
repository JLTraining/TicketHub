using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketHub.Data;
using TicketHub.Models;

namespace TicketHub.Controllers
{
    public class EventLocationsController : Controller
    {
        private readonly TicketHubContext _context;

        public EventLocationsController(TicketHubContext context)
        {
            _context = context;
        }

        // GET: EventLocations
        public async Task<IActionResult> Index()
        {
            var ticketHubContext = _context.EventLocation.Include(e => e.Event).Include(e => e.Location);
            return View(await ticketHubContext.ToListAsync());
        }

        // GET: EventLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventLocation == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocation
                .Include(e => e.Event)
                .Include(e => e.Location)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventLocation == null)
            {
                return NotFound();
            }

            return View(eventLocation);
        }

        // GET: EventLocations/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title");
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "City");
            return View();
        }

        // POST: EventLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,LocationId")] EventLocation eventLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", eventLocation.EventId);
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "City", eventLocation.LocationId);
            return View(eventLocation);
        }

        // GET: EventLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventLocation == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocation.FindAsync(id);
            if (eventLocation == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", eventLocation.EventId);
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "City", eventLocation.LocationId);
            return View(eventLocation);
        }

        // POST: EventLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,LocationId")] EventLocation eventLocation)
        {
            if (id != eventLocation.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventLocationExists(eventLocation.EventId))
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
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Title", eventLocation.EventId);
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "City", eventLocation.LocationId);
            return View(eventLocation);
        }

        // GET: EventLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventLocation == null)
            {
                return NotFound();
            }

            var eventLocation = await _context.EventLocation
                .Include(e => e.Event)
                .Include(e => e.Location)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventLocation == null)
            {
                return NotFound();
            }

            return View(eventLocation);
        }

        // POST: EventLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventLocation == null)
            {
                return Problem("Entity set 'TicketHubContext.EventLocation'  is null.");
            }
            var eventLocation = await _context.EventLocation.FindAsync(id);
            if (eventLocation != null)
            {
                _context.EventLocation.Remove(eventLocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventLocationExists(int id)
        {
          return (_context.EventLocation?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
