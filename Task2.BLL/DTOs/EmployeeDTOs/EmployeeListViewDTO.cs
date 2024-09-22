using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.TitleDTOs;

namespace Task2.BLL.DTOs.EmployeeDTOs
{
	public class EmployeeListViewDTO
	{
		public EmployeeListViewDTO()
		{
			EmployeeViewDTOs = new List<EmployeeViewDTO>();
		}
		public IEnumerable<EmployeeViewDTO> EmployeeViewDTOs { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
	}
}
