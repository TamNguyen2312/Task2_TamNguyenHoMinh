using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.StoreDTOs;
using Task2.BLL.DTOs.TitleDTOs;
using Task2.BLL.Helpers.Extensions.Titles;
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

        public async Task<TitleDetailDTO> GetTitleByIdAsync(string id)
        {
            var title = await unitOfWork.GetRepo<Title>().GetSingle(x => x.TitleId.Equals(id), null, false, x => x.Sales, x => x.Titleauthors);
            var titleResonse = mapper.Map<TitleDetailDTO>(title);
            return titleResonse;
        }

		public async Task<TitleDetailDTO> CreateTitleAsync(TitleCreateRequestDTO titleRequest)
		{
            try
            {
				using var transaction = unitOfWork.BeginTransactionAsync();

				var titleRepo = unitOfWork.GetRepo<Title>();

				string titleId;
                do
                {
                    titleId = TitleExtensions.GenerateTitleId(titleRequest.Type.ToString());
                } while (await titleRepo.GetSingle(s => s.TitleId == titleId) != null);

				var title = mapper.Map<Title>(titleRequest);
				title.TitleId = titleId;

				var createResult = await titleRepo.CreateAsync(title);
				await unitOfWork.SaveChangesAsync();
				await unitOfWork.CommitTransactionAsync();
                return mapper.Map<TitleDetailDTO>(createResult);
			}
            catch (Exception ex)
            {
				await unitOfWork.RollBackAsync();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message.ToString());
                Console.ResetColor();
                return null;
			}
		}

		public async Task<TitleDetailDTO> UpdateTitleAsync(TitleDetailDTO titleRequest)
		{
			try
            {
				using var transaction = unitOfWork.BeginTransactionAsync();

				var titleRepo = unitOfWork.GetRepo<Title>();

                var titleUpdate = mapper.Map<Title>(titleRequest);
                await titleRepo.UpdateAsync(titleUpdate);

				await unitOfWork.SaveChangesAsync();
				await unitOfWork.CommitTransactionAsync();
                return mapper.Map<TitleDetailDTO>(titleUpdate);
			}
            catch (Exception ex)
            {
				await unitOfWork.RollBackAsync();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message.ToString());
				Console.ResetColor();
                return null;
			}
		}

		public async Task<bool> DeleteTitleAsync(TitleDetailDTO titleRequest)
		{
            try
            {
                using var transaction = unitOfWork.BeginTransactionAsync();
                var titleRepo = unitOfWork.GetRepo<Title>();
                var titleDelte = mapper.Map<Title>(titleRequest);
				await titleRepo.DeleteAsync(titleDelte);
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
