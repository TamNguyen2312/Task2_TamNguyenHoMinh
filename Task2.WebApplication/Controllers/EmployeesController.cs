using Microsoft.AspNetCore.Mvc;
using Task2.BLL.Services.Implement;
using Task2.BLL.Services.Interface;

namespace Task2.WebApplicationMVC.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly IEmployeeService employeeService;

		public EmployeesController(IEmployeeService employeeService)
        {
			this.employeeService = employeeService;
		}
        public async Task<IActionResult> Index(string search, int page = 1)
		{
			try
			{
				var results = await employeeService.GetAllEmployeeAsync(search, page);
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
	}
}
