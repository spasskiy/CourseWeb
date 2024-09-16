using CourseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourseWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(CardKeeper.Head is null)
            {
                CardKeeper.Head = new Card
                {
                    Front = "Привет",
                    Back = "Hello",
                    Priority = 1
                };

                Card.AddCard(CardKeeper.Head, "Мир", "World", 1);
                Card.AddCard(CardKeeper.Head, "Кот", "Cat", 1);
                Card.AddCard(CardKeeper.Head, "Работа", "Work", 1);
                Card.AddCard(CardKeeper.Head, "Дом", "Home", 1);
            }
            return View(CardKeeper.Head);
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
            if (CardKeeper.Head is not null)
                CardKeeper.Head = CardKeeper.Head.MoveNext(CardKeeper.Head);
            return Json(CardKeeper.Head);
        }

        [HttpPost]
        public IActionResult NotRememberClick()
        {
            if(CardKeeper.Head is not null)
                CardKeeper.Head = CardKeeper.Head.MoveNext(CardKeeper.Head, true);
            return Json(CardKeeper.Head);
        }

        [HttpPost]
        public IActionResult DropCard()
        {
            if (CardKeeper.Head is not null)
                CardKeeper.Head = CardKeeper.Head.Next;
            return Json(CardKeeper.Head);
    }
    }
}
