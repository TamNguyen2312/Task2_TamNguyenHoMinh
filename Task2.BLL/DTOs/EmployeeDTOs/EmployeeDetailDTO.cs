using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.Helpers.Extensions.Employees;

namespace Task2.BLL.DTOs.EmployeeDTOs
{
	public class EmployeeDetailDTO
	{
        public string EmpId { get; set; } = null!;

        [Required(ErrorMessage = "First name is required")]
		[StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters")]
		public string Fname { get; set; } = null!;

		[StringLength(1, ErrorMessage = "Middle initial cannot be longer than 1 character")]
		public string? Minit { get; set; }

		[Required(ErrorMessage = "Last name is required")]
		[StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters")]
		public string Lname { get; set; } = null!;

		[Required(ErrorMessage = "Gender is required")]
		public EmpGender Gender { get; set; }

		[Required(ErrorMessage = "Job ID is required")]
		public short JobId { get; set; }

		[Range(1, 255, ErrorMessage = "Job level must be between 1 and 255")]
		[Required(ErrorMessage = "Job level is required")]
		public byte? JobLvl { get; set; }

		[Required(ErrorMessage = "Publisher ID is required")]
		[StringLength(4, ErrorMessage = "Publisher ID cannot be longer than 4 characters")]
		public string PubId { get; set; } = null!;

		[Required(ErrorMessage = "Hire date is required")]
		[DataType(DataType.Date)]
		public DateTime HireDate { get; set; }
	}
}
