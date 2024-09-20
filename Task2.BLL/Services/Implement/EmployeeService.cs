using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.EmployeeDTOs;
using Task2.BLL.DTOs.TitleDTOs;
using Task2.BLL.Helpers.Filters;
using Task2.BLL.Helpers.Paging;
using Task2.BLL.Services.Interface;
using Task2.DAL.Entities;
using Task2.DAL.Repositories;

namespace Task2.BLL.Services.Implement
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public static int PAGE_SIZE { get; set; } = 7;

		public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}
        public async Task<EmployeeListViewDTO> GetAllEmployeeAsync(string search, int page)
		{
			var empRepo = unitOfWork.GetRepo<Employee>();
			IEnumerable<Employee> emps = new List<Employee>();
			if (!String.IsNullOrEmpty(search))
			{
				var predicate = FilterHelper.BuildSearchExpression<Employee>(search);

				emps = await empRepo.GetAllAsync(predicate,
					r => r.OrderBy(q => q.Fname),
					false
				);
			}
			else
			{

				emps = await empRepo.GetAllAsync(
					null,
					r => r.OrderBy(q => q.Fname),
					false
				);

			}

			var results = mapper.Map<List<EmployeeViewDTO>>(emps);
			var pageResults = PaginatedList<EmployeeViewDTO>.Create(results, page, PAGE_SIZE);

			return new EmployeeListViewDTO
			{
				EmployeeViewDTOs = pageResults,
				CurrentPage = page,
				TotalPages = pageResults.TotalPage,
			};
		}

		public async Task<EmployeeDetailDTO> GetEmployeeById(string id)
		{
			var empRepo = unitOfWork.GetRepo<Employee>();

			var emp = await empRepo.GetSingle(x => x.EmpId.Equals(id), null, false, 
												x => x.Pub, x => x.Job);

			var empResponse = mapper.Map<EmployeeDetailDTO>(emp);
			return empResponse;
		}
	}
}
