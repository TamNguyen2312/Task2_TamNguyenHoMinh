using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.BLL.DTOs.JobDTOs
{
	public class JobViewDTO
	{
		public short JobId { get; set; }

		public string JobDesc { get; set; } = null!;

		public byte MinLvl { get; set; }

		public byte MaxLvl { get; set; }
	}
}
