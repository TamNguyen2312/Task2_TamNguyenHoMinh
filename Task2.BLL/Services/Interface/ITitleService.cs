using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.TitleDTOs;

namespace Task2.BLL.Services.Interface
{
    public interface ITitleService
    {
        public Task<TitleListViewDTO> GetAllTitlesAsync(string search, int page);
        public Task<TitleDetailDTO> GetTitleByIdAsync(string id);
    }
}
