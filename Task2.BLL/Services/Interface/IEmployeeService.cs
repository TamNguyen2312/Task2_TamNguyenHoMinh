using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.EmployeeDTOs;

namespace Task2.BLL.Services.Interface
{
	public interface IEmployeeService
	{
		public Task<EmployeeListViewDTO> GetAllEmployeeAsync(string search, int page);
		public Task<EmployeeDetailDTO> GetEmployeeById(string id);
	}
}
