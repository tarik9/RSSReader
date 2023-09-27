using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalRSSReader.Data; // Import the ApplicationDbContext
using MinimalRSSReader.Models; // Import your model
using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace MinimalRSSReader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allFeeds = await _context.RSSFeeds.ToListAsync(); // Retrieve all RSS feeds from the database
            return View(allFeeds);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string RSSURL)
        {
            try
            {
                var feedData = await GetRssFeedData(RSSURL);
                ViewBag.RSSFeed = feedData;
                ViewBag.URL = RSSURL;

                return View("Index", await _context.RSSFeeds.ToListAsync());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error fetching or parsing the RSS feed: " + ex.Message;
                return View("Index", await _context.RSSFeeds.ToListAsync());
            }
        }

        private async Task<List<SyndicationItem>> GetRssFeedData(string RSSURL)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var rssData = await httpClient.GetStringAsync(RSSURL);

                using (var stringReader = new System.IO.StringReader(rssData))
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    var feed = SyndicationFeed.Load(xmlReader);
                    return feed.Items.ToList();
                }
            }
        }

        // GET: Home/Delete/5
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

        // POST: Home/Delete/5
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

            return RedirectToAction("Index"); // Redirect to the home page
        }

        // Other actions in your HomeController
    }
}
