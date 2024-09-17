using Microsoft.AspNetCore.Mvc;
using Task2.BLL.Services.Interface;

namespace Task2.WebApplicationMVC.Controllers
{
	public class StoresController : Controller
	{
		private readonly IStoresService storesService;

		public StoresController(IStoresService storesService)
        {
			this.storesService = storesService;
		}
        public async Task<IActionResult> Index(int page = 1)
		{
			var results = await storesService.GetAllStoresAsync(page);
			return View(results);
		}
	}
}
