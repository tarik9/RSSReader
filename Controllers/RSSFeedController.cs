using Microsoft.AspNetCore.Mvc;
using MinimalRSSReader.Data; // Import the ApplicationDbContext
using MinimalRSSReader.Models; // Import your model
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ReadRSSFeed.Controllers
{
    public class RSSFeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RSSFeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RSSFeed/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: RSSFeed/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(RSSFeed feed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feed);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home"); // Redirect to the home page or a feed listing page
            }
            return View(feed);
        }

        // GET: RSSFeed/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feed = await _context.RSSFeeds.FirstOrDefaultAsync(m => m.Id == id);

            if (feed == null)
            {
                return NotFound();
            }

            return View(feed);
        }

        // POST: RSSFeed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feed = await _context.RSSFeeds.FindAsync(id);

            if (feed == null)
            {
                return NotFound();
            }

            _context.RSSFeeds.Remove(feed);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home"); // Redirect to the home page or a feed listing page
        }
    }
}
