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
    public class PurchaseHistoriesController : Controller
    {
        private readonly TicketHubContext _context;

        public PurchaseHistoriesController(TicketHubContext context)
        {
            _context = context;
        }

        // GET: PurchaseHistories
        public async Task<IActionResult> Index()
        {
            var ticketHubContext = _context.PurchaseHistory.Include(p => p.Ticket).Include(p => p.User);
            return View(await ticketHubContext.ToListAsync());
        }

        // GET: PurchaseHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurchaseHistory == null)
            {
                return NotFound();
            }

            var purchaseHistory = await _context.PurchaseHistory
                .Include(p => p.Ticket)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return View(purchaseHistory);
        }

        // GET: PurchaseHistories/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Alias");
            return View();
        }

        // POST: PurchaseHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TicketId,Date,Price")] PurchaseHistory purchaseHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", purchaseHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Alias", purchaseHistory.UserId);
            return View(purchaseHistory);
        }

        // GET: PurchaseHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurchaseHistory == null)
            {
                return NotFound();
            }

            var purchaseHistory = await _context.PurchaseHistory.FindAsync(id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", purchaseHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Alias", purchaseHistory.UserId);
            return View(purchaseHistory);
        }

        // POST: PurchaseHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TicketId,Date,Price")] PurchaseHistory purchaseHistory)
        {
            if (id != purchaseHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseHistoryExists(purchaseHistory.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", purchaseHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Alias", purchaseHistory.UserId);
            return View(purchaseHistory);
        }

        // GET: PurchaseHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchaseHistory == null)
            {
                return NotFound();
            }

            var purchaseHistory = await _context.PurchaseHistory
                .Include(p => p.Ticket)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return View(purchaseHistory);
        }

        // POST: PurchaseHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchaseHistory == null)
            {
                return Problem("Entity set 'TicketHubContext.PurchaseHistory'  is null.");
            }
            var purchaseHistory = await _context.PurchaseHistory.FindAsync(id);
            if (purchaseHistory != null)
            {
                _context.PurchaseHistory.Remove(purchaseHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseHistoryExists(int id)
        {
          return (_context.PurchaseHistory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
