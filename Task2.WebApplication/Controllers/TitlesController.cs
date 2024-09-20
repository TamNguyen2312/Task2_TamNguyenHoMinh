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
            try
            {
				var results = await titleService.GetAllTitlesAsync(search, page);
				ViewBag.Search = search;
				return View(results);
			}
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ResetColor();
                return Redirect("/400");
            }
        }

        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            try
            {
				var results = await titleService.GetTitleByIdAsync(id);
				if (results == null)
				{
					return Redirect("/404");
				}
				return View(results);
			}
            catch (Exception ex)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ResetColor();
                return Redirect("/400");
            }
        }
    }
}
