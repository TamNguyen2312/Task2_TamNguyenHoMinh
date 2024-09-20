using Microsoft.AspNetCore.Mvc;
using Task2.BLL.DTOs.StoreDTOs;
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
        public async Task<IActionResult> Index(string search, int page = 1)
		{
			var results = await storesService.GetAllStoresAsync(search, page);
			ViewBag.Search = search;
			return View(results);
		}

		public async Task<IActionResult> Detail([FromRoute]string id)
		{
			var results = await storesService.GetStoreByIdAsync(id);
			if (results == null)
			{
				return Redirect("/404");
			}
			return View(results);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(StoreCreateRequestDTO storeCreateRequest)
		{
			try
			{
                if (!ModelState.IsValid)
				{
					return View(storeCreateRequest);
				}
				else
				{
					var result = await storesService.CreateStoreAsync(storeCreateRequest);
					if(result == null)
					{
						return Redirect("/400");
					}
					else
					{
						return RedirectToAction("Detail", new {id = result.StorId});
					}
				}

            }
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ResetColor();
				return Redirect("/400");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Update(string id)
		{
			try
			{
				var store = await storesService.GetStoreByIdAsync(id);
				if(store == null)
				{
					return Redirect("/404");
				}
				else
				{
					return View(store);
				}
			}
			catch (Exception ex)
			{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return Redirect("/400");
            }
		}

		[HttpPost]
		public async Task<IActionResult> Update(StoreDetailDTO storeDetail)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					return View(storeDetail);
				}
				else
				{
					await storesService.UpdateStoreAsync(storeDetail);
					return RedirectToAction("Detail", new { id = storeDetail.StorId });
				}
			}
			catch (Exception ex)
			{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return Redirect("/400");
            }
		}

		public async Task<IActionResult> Delete(string id)
		{
            try
            {
				var store = await storesService.GetStoreByIdAsync(id);
				if(store == null)
				{
					return Redirect("/404");
				}

				if(store.Sales.Any())
				{
					TempData["ErrorMessage"] = "Cannot delete store. There are asscoiated sales";
					return Redirect("/400");
				}

				var result = await storesService.DeleteStoreAsync(store); 
                if (!result)
                {
                    return Redirect("/400");
                }
                else
                {
					return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return Redirect("/400");
            }
        }
	}
}
