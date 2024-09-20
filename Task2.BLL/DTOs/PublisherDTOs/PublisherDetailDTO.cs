using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DAL.Entities;

namespace Task2.BLL.DTOs.PublisherDTOs
{
	public class PublisherDetailDTO
	{
        public PublisherDetailDTO()
        {
			Employees = new List<Employee>();
			Titles = new List<Title>();
		}
        public string PubId { get; set; } = null!;

		public string? PubName { get; set; }

		public string? City { get; set; }

		public string? State { get; set; }

		public string? Country { get; set; }

		public ICollection<Employee> Employees { get; set; }

		public PubInfo? PubInfo { get; set; }

		public ICollection<Title> Titles { get; set; }
	}
}
