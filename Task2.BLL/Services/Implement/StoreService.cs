﻿using AutoMapper;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.StoreDTOs;
using Task2.BLL.Helpers.Extensions.Stores;
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
                        r => r.OrderBy(q => q.StorId),
                        false
                    );
                }
                else
                {

                    stores = await storeRepo.GetAllAsync(
                        null,
                        r => r.OrderBy(q => q.StorId),
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
        }

		public async Task<StoreDetailDTO> GetStoreByIdAsync(string id)
		{
			try
			{
				var store = await unitOfWork.GetRepo<Store>().GetSingle(x => x.StorId.Equals(id), null, false, x => x.Sales);
                var storeResonse = mapper.Map<StoreDetailDTO>(store);
                return storeResonse;
			}
			catch (Exception ex)
			{
				Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                return null;
			}
		}

        public async Task<StoreDetailDTO> CreateStoreAsync(StoreCreateRequestDTO storeRequest)
        {
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();

                var storeRepo = unitOfWork.GetRepo<Store>();

                string storeId;
                do
                {
                    storeId = StoreExtensions.AutoGenerateStoreId();
                } while (await storeRepo.GetSingle(s => s.StorId == storeId, null, false) != null);

                var store = mapper.Map<Store>(storeRequest);
                store.StorId = storeId;

                var createResult = await unitOfWork.GetRepo<Store>().CreateAsync(store);
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitTransactionAsync();

                return mapper.Map<StoreDetailDTO>(createResult);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollBackAsync();
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ResetColor();
                return null;
            }
        }

        public async Task UpdateStoreAsync(StoreDetailDTO storeRequest)
        {
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();

                var storeRepo = unitOfWork.GetRepo<Store>();

                var storeUpdate = mapper.Map<Store>(storeRequest);

                await storeRepo.UpdateAsync(storeUpdate);

                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollBackAsync();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ResetColor();
            }
        }

        public async Task<bool> DeleteStoreAsync(StoreDetailDTO storeRequest)
        {
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();

                var storeRepo = unitOfWork.GetRepo<Store>();
                var store = mapper.Map<Store>(storeRequest);
                await storeRepo.DeleteAsync(store);
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollBackAsync();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ResetColor();
                return false;
            }
        }
    }
}
