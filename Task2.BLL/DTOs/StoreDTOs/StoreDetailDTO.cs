﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DAL.Entities;

namespace Task2.BLL.DTOs.StoreDTOs
{
	public class StoreDetailDTO
	{
        public StoreDetailDTO()
        {
			Sales = new List<Sale>();
        }
        public string StorId { get; set; } = null!;

		public string? StorName { get; set; }

		public string? StorAddress { get; set; }

		public string? City { get; set; }

		public string? State { get; set; }

		public string? Zip { get; set; }

		public ICollection<Sale> Sales { get; set; }
	}
}
