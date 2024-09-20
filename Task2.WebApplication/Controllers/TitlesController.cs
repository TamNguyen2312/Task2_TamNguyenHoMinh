using Microsoft.AspNetCore.Mvc;
using Task2.BLL.Services.Implement;
using Task2.BLL.Services.Interface;

namespace Task2.WebApplicationMVC.Controllers
{
    public class TitlesController : Controller
    {
        private readonly ITitleService titleService;

        public TitlesController(ITitleService titleService)
        {
            this.titleService = titleService;
        }


        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var results = await titleService.GetAllTitlesAsync(search, page);
            ViewBag.Search = search;
            return View(results);
        }
    }
}
