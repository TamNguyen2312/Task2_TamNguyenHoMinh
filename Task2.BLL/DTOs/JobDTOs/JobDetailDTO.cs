using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DAL.Entities;

namespace Task2.BLL.DTOs.JobDTOs
{
    public class JobDetailDTO
    {
        public JobDetailDTO()
        {
            Employees = new List<Employee>();
        }
        public short JobId { get; set; }

        public string JobDesc { get; set; } = null!;

        public byte MinLvl { get; set; }

        public byte MaxLvl { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
