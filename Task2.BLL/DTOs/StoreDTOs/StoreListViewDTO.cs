using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.BLL.DTOs.StoreDTOs
{
	public class StoreListViewDTO
	{
		public IEnumerable<StoreViewDTO> StoreViewDTOs { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
	}
}
