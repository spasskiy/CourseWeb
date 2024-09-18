using CourseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace CourseWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CardContext _context;

        public HomeController(ILogger<HomeController> logger, CardContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var head = _context.Cards.OrderBy(c => c.Priority).FirstOrDefault();
            return View(head);
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

        [HttpPost]
        public IActionResult RememberClick()
        {
            var head = _context.Cards.OrderBy(c => c.Priority).FirstOrDefault();
            if (head != null)
            {
                head.Priority += 1;
                _context.SaveChanges();
            }
            return Json(head);
        }

        [HttpPost]
        public IActionResult NotRememberClick()
        {
            var head = _context.Cards.OrderBy(c => c.Priority).FirstOrDefault();
            if (head != null)
            {
                head.Priority = 1;
                _context.SaveChanges();
            }
            return Json(head);
        }

        [HttpPost]
        public IActionResult DropCard()
        {
            var head = _context.Cards.OrderBy(c => c.Priority).FirstOrDefault();
            if (head != null)
            {
                _context.Cards.Remove(head);
                _context.SaveChanges();
            }
            return Json(_context.Cards.OrderBy(c => c.Priority).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult AddCard([FromBody] Card newCard)
        {
            if (newCard != null)
            {
                newCard.Priority = 1;
                _context.Cards.Add(newCard);
                _context.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}