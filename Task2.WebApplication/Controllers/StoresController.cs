using Microsoft.AspNetCore.Mvc;

namespace Task2.WebApplicationMVC.Controllers
{
	public class StoresController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
