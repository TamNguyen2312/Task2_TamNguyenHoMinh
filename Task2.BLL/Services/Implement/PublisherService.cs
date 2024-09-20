using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.PublisherDTOs;
using Task2.BLL.Services.Interface;
using Task2.DAL.Entities;
using Task2.DAL.Repositories;

namespace Task2.BLL.Services.Implement
{
	public class PublisherService : IPublisherService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}
        public async Task<IEnumerable<PublisherViewDTO>> GetComboboxPublisher()
		{
			var publisherRepo = unitOfWork.GetRepo<Publisher>();

			var pubs = await publisherRepo.GetAllAsync(null, x => x.OrderBy(y => y.PubName), false);

			var responsePubs = mapper.Map<List<PublisherViewDTO>>(pubs);
			unitOfWork.Dispose();
			return responsePubs;
			
		}
	}
}
