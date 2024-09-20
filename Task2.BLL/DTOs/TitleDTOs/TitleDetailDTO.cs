using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.Helpers.Extensions.Titles;
using Task2.BLL.Helpers.Validation.TitleValid;
using Task2.DAL.Entities;

namespace Task2.BLL.DTOs.TitleDTOs
{
    public class TitleDetailDTO
    {
        public TitleDetailDTO()
        {
            Sales = new List<Sale>();
            Titleauthors = new List<Titleauthor>();
        }
        [Display(Name = "ID")]
        public string TitleId { get; set; }

        [Required(ErrorMessage = "Title1 is required")]
        [StringLength(80, ErrorMessage = "Title1 cannot be longer than 80 characters")]
        [Display(Name = "Title")]
        public string Title1 { get; set; } = null!;

        [Required(ErrorMessage = "Type is required")]
        [TypeValid(typeof(TitleTypes))]
        [Display(Name = "Type")]
        public string Type { get; set; } = null!;

        [StringLength(4, ErrorMessage = "PubId cannot be longer than 4 characters")]
        [Display(Name = "Publisher")]
        public string? PubId { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Price must be a positive value")]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Advance must be a positive value")]
        [Display(Name = "Advance")]
        public decimal? Advance { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Royalty must be a non-negative integer")]
        [Display(Name = "Royalty")]
        public int? Royalty { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "YtdSales must be a non-negative integer")]
        [Display(Name = "YtdSales")]
        public int? YtdSales { get; set; }

        [StringLength(200, ErrorMessage = "Notes cannot be longer than 200 characters")]
        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Pubdate is required")]
        [Display(Name = "Publish Date")]
        public DateTime Pubdate { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Titleauthor> Titleauthors { get; set; }
    }
}
