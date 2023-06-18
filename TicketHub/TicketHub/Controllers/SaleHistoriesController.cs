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
    public class SaleHistoriesController : Controller
    {
        private readonly TicketHubContext _context;

        public SaleHistoriesController(TicketHubContext context)
        {
            _context = context;
        }

        // GET: SaleHistories
        public async Task<IActionResult> Index()
        {
            var ticketHubContext = _context.SaleHistory.Include(s => s.Ticket).Include(s => s.User);
            return View(await ticketHubContext.ToListAsync());
        }

        // GET: SaleHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SaleHistory == null)
            {
                return NotFound();
            }

            var saleHistory = await _context.SaleHistory
                .Include(s => s.Ticket)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleHistory == null)
            {
                return NotFound();
            }

            return View(saleHistory);
        }

        // GET: SaleHistories/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Alias");
            return View();
        }

        // POST: SaleHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TicketId,Date,Price")] SaleHistory saleHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", saleHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Alias", saleHistory.UserId);
            return View(saleHistory);
        }

        // GET: SaleHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SaleHistory == null)
            {
                return NotFound();
            }

            var saleHistory = await _context.SaleHistory.FindAsync(id);
            if (saleHistory == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", saleHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Alias", saleHistory.UserId);
            return View(saleHistory);
        }

        // POST: SaleHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TicketId,Date,Price")] SaleHistory saleHistory)
        {
            if (id != saleHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleHistoryExists(saleHistory.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", saleHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Alias", saleHistory.UserId);
            return View(saleHistory);
        }

        // GET: SaleHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SaleHistory == null)
            {
                return NotFound();
            }

            var saleHistory = await _context.SaleHistory
                .Include(s => s.Ticket)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleHistory == null)
            {
                return NotFound();
            }

            return View(saleHistory);
        }

        // POST: SaleHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SaleHistory == null)
            {
                return Problem("Entity set 'TicketHubContext.SaleHistory'  is null.");
            }
            var saleHistory = await _context.SaleHistory.FindAsync(id);
            if (saleHistory != null)
            {
                _context.SaleHistory.Remove(saleHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleHistoryExists(int id)
        {
          return (_context.SaleHistory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
