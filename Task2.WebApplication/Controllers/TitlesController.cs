using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task2.BLL.DTOs.TitleDTOs;
using Task2.BLL.Services.Implement;
using Task2.BLL.Services.Interface;

namespace Task2.WebApplicationMVC.Controllers
{
    public class TitlesController : Controller
    {
        private readonly ITitleService titleService;
		private readonly IPublisherService publisherService;

		public TitlesController(ITitleService titleService, IPublisherService publisherService)
        {
            this.titleService = titleService;
			this.publisherService = publisherService;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var publishers = await publisherService.GetComboboxPublisher();
			if (publishers == null || !publishers.Any())
			{
                return Redirect("/404");
			}
			ViewBag.PublisherList = new SelectList(publishers, "PubId", "PubName");
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(TitleCreateRequestDTO titleRequest)
		{
            try
            {
                if(!ModelState.IsValid)
                {
                    var publishers = await publisherService.GetComboboxPublisher();
					ViewBag.PublisherList = new SelectList(publishers, "PubId", "PubName");
                    return View(titleRequest);
				}
                else
                {
                    var result = await titleService.CreateTitleAsync(titleRequest);
                    return RedirectToAction("Detail", new {id  = result.TitleId});
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ResetColor();
                return Redirect("/400");
            }
		}
	}
}
