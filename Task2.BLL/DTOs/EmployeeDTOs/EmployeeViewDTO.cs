using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.BLL.DTOs.EmployeeDTOs
{
	public class EmployeeViewDTO
	{
		public string EmpId { get; set; } = null!;

		public string Fname { get; set; } = null!;

		public string? Minit { get; set; }

		public string Lname { get; set; } = null!;

		public short JobId { get; set; }

		public byte? JobLvl { get; set; }

		public string PubId { get; set; } = null!;

		[DataType(DataType.Date)]
		public DateTime HireDate { get; set; }
	}
}
