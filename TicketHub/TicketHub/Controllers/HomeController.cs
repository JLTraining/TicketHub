using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketHub.Areas.Identity.Data;
using TicketHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.Extensions.Localization;

namespace TicketHub.Controllers
{
	public class HomeController : Controller
	{
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;
		public HomeController( ApplicationDbContext context, 
            IStringLocalizer<HomeController> localizer)
		{

			_context = context;
            _localizer = localizer;
		}

        public IActionResult SetCulture(string culture)
        {
            // Store the selected culture in the session
            HttpContext.Session.SetString("lv-LV", culture);

            // Redirect to the previous page or a specific page
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Index(string sortOrder, string search)
        {
            var localizedTitle = _localizer["Welcome"];
            var tickets = _context.Ticket.Include(t => t.Event).Include(t => t.Seller).Where(t => t.isListed == true);

            ViewData["TitleSortParam"] = sortOrder == "TitleAsc" ? "TitleDesc" : "TitleAsc";
            ViewData["PriceSortParam"] = sortOrder == "PriceAsc" ? "PriceDesc" : "PriceAsc";

            switch (sortOrder)
            {
                case "TitleAsc":
                    tickets = tickets.OrderBy(keySelector: t => t.Event.Title);
                    break;
                case "TitleDesc":
                    tickets = tickets.OrderByDescending(t => t.Event.Title);
                    break;
                case "PriceAsc":
                    tickets = tickets.OrderBy(t => t.Price);
                    break;
                case "PriceDesc":
                    tickets = tickets.OrderByDescending(t => t.Price);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.Event.Title);
                    break;
            }

            if (!string.IsNullOrEmpty(search))
            {
                tickets = tickets.Where(t => t.Event.Title.Contains(search) || t.Event.Description.Contains(search));
            }

            return View(tickets.ToList());
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
        // GET
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
                ticket.isListed = false;
                _context.Update(ticket);
                _context.SaveChanges();
                

                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        //Buy remove listing
        // GET
        public IActionResult RemoveFromList(int? id)

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

        // POST: Tickets/RemoveFromList/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromList(int id)
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ticket = _context.Ticket.Find(id);

            if (ticket == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ticket.isListed = false;
                _context.Update(ticket);
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

    }
}