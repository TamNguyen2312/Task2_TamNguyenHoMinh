using AutoMapper;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.StoreDTOs;
using Task2.BLL.Helpers.Filters;
using Task2.BLL.Helpers.Paging;
using Task2.BLL.Services.Interface;
using Task2.DAL.Entities;
using Task2.DAL.Repositories;

namespace Task2.BLL.Services.Implement
{
    public class StoreService : IStoresService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public static int PAGE_SIZE { get; set; } = 2;

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<StoreListViewDTO> GetAllStoresAsync(string search, int page)
        {
            try
            {
                var storeRepo = unitOfWork.GetRepo<Store>();
                IEnumerable<Store> stores = new List<Store>();
                if (!String.IsNullOrEmpty(search))
                {
                    var predicate = FilterHelper.BuildSearchExpression<Store>(search);

                    stores = await storeRepo.GetAllAsync(predicate,
                        r => r.OrderBy(q => q.StorName),
                        false
                    );
                }
                else
                {

                    stores = await storeRepo.GetAllAsync(
                        null,
                        r => r.OrderBy(q => q.StorName),
                        false
                    );

                }

                var results = mapper.Map<List<StoreViewDTO>>(stores);
                var pageResults = PaginatedList<StoreViewDTO>.Create(results, page, PAGE_SIZE);
                return new StoreListViewDTO
                {
                    StoreViewDTOs = pageResults,
                    CurrentPage = page,
                    TotalPages = pageResults.TotalPage
                };
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return null;
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }
    }
}
