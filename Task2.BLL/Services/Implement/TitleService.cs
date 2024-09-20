using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.StoreDTOs;
using Task2.BLL.DTOs.TitleDTOs;
using Task2.BLL.Helpers.Filters;
using Task2.BLL.Helpers.Paging;
using Task2.BLL.Services.Interface;
using Task2.DAL.Entities;
using Task2.DAL.Repositories;

namespace Task2.BLL.Services.Implement
{
    public class TitleService : ITitleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public static int PAGE_SIZE { get; set; } = 6;
        public TitleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<TitleListViewDTO> GetAllTitlesAsync(string search, int page)
        {
            var titleRepo = unitOfWork.GetRepo<Title>();
            IEnumerable<Title> titles = new List<Title>();
            if (!String.IsNullOrEmpty(search))
            {
                var predicate = FilterHelper.BuildSearchExpression<Title>(search);

                titles = await titleRepo.GetAllAsync(predicate,
                    r => r.OrderBy(q => q.Title1),
                    false
                );
            }
            else
            {

                titles = await titleRepo.GetAllAsync(
                    null,
                    r => r.OrderBy(q => q.Title1),
                    false
                );

            }

            var results = mapper.Map<List<TitleViewDTO>>(titles);
            var pageResults = PaginatedList<TitleViewDTO>.Create(results, page, PAGE_SIZE);
            return new TitleListViewDTO
            {
                TitleViewDTOs = pageResults,
                CurrentPage = page,
                TotalPages = pageResults.TotalPage
            };
        }
    }
}
