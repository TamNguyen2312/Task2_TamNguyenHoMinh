using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.JobDTOs;

namespace Task2.BLL.Services.Interface
{
    public interface IJobService
    {
        public Task<JobDetailDTO> GetJobByIdAsync(short id);
        public Task<IEnumerable<JobViewDTO>> GetComboBoxJob();
    }
}
