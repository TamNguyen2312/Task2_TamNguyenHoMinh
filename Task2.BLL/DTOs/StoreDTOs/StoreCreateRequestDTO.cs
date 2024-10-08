﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.Helpers.Validation.StoreValid;

namespace Task2.BLL.DTOs.StoreDTOs
{
    public class StoreCreateRequestDTO
    {
        [Display(Name ="Name")]
        [StringLength(40, ErrorMessage = "Store Name cannot be longer than 40")]
        public string? StorName { get; set; }

        [Display(Name = "Address")]
        [StringLength(40, ErrorMessage = "Store Address cannot be longer than 40")]
        public string? StorAddress { get; set; }

        [Display(Name = "City")]
        [StringLength(20, ErrorMessage = "City cannot be longer than 20")]
        public string? City { get; set; }

		[Display(Name = "State")]
		[StringLength(2, ErrorMessage = "State cannot be longer than 2")]
        public string? State { get; set; }

		[Display(Name = "Zip")]
		[StringLength(5, ErrorMessage = "Zip cannot be longer than 5")]
        [ZipValid(ErrorMessage = "Invalid type specified")]
        public string? Zip { get; set; }
    }
}
