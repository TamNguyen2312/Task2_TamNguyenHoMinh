using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.StoreDTOs;

namespace Task2.BLL.DTOs.TitleDTOs
{
    public class TitleListViewDTO
    {
        public TitleListViewDTO()
        {
            TitleViewDTOs = new List<TitleViewDTO>();
        }
        public IEnumerable<TitleViewDTO> TitleViewDTOs { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
