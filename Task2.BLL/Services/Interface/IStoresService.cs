using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.StoreDTOs;

namespace Task2.BLL.Services.Interface
{
	public interface IStoresService
	{
		Task<StoreListViewDTO> GetAllStoresAsync(string search, int page);
		Task<StoreDetailDTO> GetStoreByIdAsync(string id);
		Task<StoreDetailDTO> CreateStoreAsync(StoreCreateRequestDTO storeRequest);
		Task UpdateStoreAsync(StoreDetailDTO storeRequest);
		Task<bool> DeleteStoreAsync(StoreDetailDTO storeRequest);
	}
}
