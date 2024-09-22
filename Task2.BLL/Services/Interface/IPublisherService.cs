using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.PublisherDTOs;

namespace Task2.BLL.Services.Interface
{
	public interface IPublisherService
	{
		Task<IEnumerable<PublisherViewDTO>> GetComboboxPublisher();
		Task<PublisherDetailDTO> GetPublisherByIdAsync(string id);
	}
}
