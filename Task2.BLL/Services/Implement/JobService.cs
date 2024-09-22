using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.JobDTOs;
using Task2.BLL.DTOs.TitleDTOs;
using Task2.BLL.Services.Interface;
using Task2.DAL.Entities;
using Task2.DAL.Repositories;

namespace Task2.BLL.Services.Implement
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public JobService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

		public async Task<IEnumerable<JobViewDTO>> GetComboBoxJob()
		{
			var job = await unitOfWork.GetRepo<Job>().GetAllAsync(null, x => x.OrderBy(x => x.JobId), true);
            return mapper.Map<List<JobViewDTO>>(job);
		}

		public async Task<JobDetailDTO> GetJobByIdAsync(short id)
        {
            var job = await unitOfWork.GetRepo<Job>().GetSingle(x => x.JobId == id, null, false, x => x.Employees);
            if(job == null)
            {
                return null;
            }
            var jobResonse = mapper.Map<JobDetailDTO>(job);
            return jobResonse;
        }
    }
}
