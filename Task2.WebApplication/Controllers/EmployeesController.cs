using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task2.BLL.DTOs.EmployeeDTOs;
using Task2.BLL.DTOs.TitleDTOs;
using Task2.BLL.Helpers.Extensions.Exceptions;
using Task2.BLL.Services.Implement;
using Task2.BLL.Services.Interface;

namespace Task2.WebApplicationMVC.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly IEmployeeService employeeService;
		private readonly IPublisherService publisherService;
		private readonly IJobService jobService;

		public EmployeesController(IEmployeeService employeeService, IPublisherService publisherService, IJobService jobService)
		{
			this.employeeService = employeeService;
			this.publisherService = publisherService;
			this.jobService = jobService;
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

		[HttpGet]
		public async Task<IActionResult> Detail([FromRoute] string id)
		{
			try
			{
				var results = await employeeService.GetEmployeeById(id);
				if (results == null)
				{
					return Redirect("/404");
				}
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

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			await LoadDataToForm();
			var model = new EmployeeCreateRequestDTO();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(EmployeeCreateRequestDTO empRequest)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					await LoadDataToForm();
					return View(empRequest);
				}

				var job = await jobService.GetJobByIdAsync(empRequest.JobId);
				if (job == null)
				{
					empRequest.JobId = 1;
					empRequest.JobLvl = 10;
				}
				else
				{

					if (empRequest.JobLvl < job.MinLvl || empRequest.JobLvl > job.MaxLvl)
					{
						ModelState.AddModelError("JobLvl", $"Job Id {empRequest.JobId} has the range of Job level from {job.MinLvl} to {job.MaxLvl}");
						await LoadDataToForm();
						return View(empRequest);
					}

				}

				var publisher = await publisherService.GetPublisherByIdAsync(empRequest.PubId);
				if (publisher == null)
				{
					empRequest.PubId = "9952";
				}

				var result = await employeeService.CreateEmployeeAsync(empRequest);
				if (result == null)
				{
					TempData["ErrorMessgae"] = "Created employeee failed. Please try again after a few minute";
					return Redirect("/400");
				}

				return RedirectToAction("Detail", new { id = result.EmpId });
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message.ToString());
				Console.ResetColor();
				TempData["ErrorMessage"] = "The procees have any errors. Please try again after a few minutes.";
				return Redirect("/400");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Update(string id)
		{
			try
			{
				var emp = await employeeService.GetEmployeeById(id);
				if (emp == null)
				{
					TempData["ErrorMessage"] = "Employee not found";
					return Redirect("/404");
				}
				else
				{
					await LoadDataToForm();
					return View(emp);
				}
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ResetColor();
				TempData["ErrorMessage"] = "The procees have any errors. Please try again after a few minutes.";
				return Redirect("/400");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Update(EmployeeDetailDTO empRequest)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					await LoadDataToForm();
					return View(empRequest);
				}

				var job = await jobService.GetJobByIdAsync(empRequest.JobId);
				if (job == null)
				{
					empRequest.JobId = 1;
					empRequest.JobLvl = 10;
				}
				else
				{

					if (empRequest.JobLvl < job.MinLvl || empRequest.JobLvl > job.MaxLvl)
					{
						ModelState.AddModelError("JobLvl", $"Job Id {empRequest.JobId} has the range of Job level from {job.MinLvl} to {job.MaxLvl}");
						await LoadDataToForm();
						return View(empRequest);
					}

				}

				var publisher = await publisherService.GetPublisherByIdAsync(empRequest.PubId);
				if (publisher == null)
				{
					empRequest.PubId = "9952";
				}

				var result = await employeeService.UpdateEmployeeAsync(empRequest);
				if (result == null)
				{
					return Redirect("/400");
				}
				else
				{
					return RedirectToAction("Detail", new { id = result.EmpId });
				}
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ResetColor();
				TempData["ErrorMessage"] = "The procees have any errors. Please try again after a few minutes.";
				return Redirect("/400");
			}
		}

		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				var emp = await employeeService.GetEmployeeById(id);
				if (emp == null)
				{
					TempData["ErrorMessage"] = "Employee Not Found";
					return Redirect("/404");
				}

				var result = await employeeService.DeleteEmployeeAsync(emp);
				if (!result)
				{
					TempData["ErrorMessage"] = "Deleted Title Failed";
					return Redirect("/400");
				}

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ResetColor();
				TempData["ErrorMessage"] = "The procees have any errors. Please try again after a few minutes.";
				return Redirect("/400");
			}
		}
		private async Task LoadDataToForm()
		{
			var publishers = await publisherService.GetComboboxPublisher();
			ViewBag.PublisherList = new SelectList(publishers, "PubId", "PubName");

			var jobs = await jobService.GetComboBoxJob();
			ViewBag.JobList = new SelectList(jobs, "JobId", "JobId");
		}
	}
}
