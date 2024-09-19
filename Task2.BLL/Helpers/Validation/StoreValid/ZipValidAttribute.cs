using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task2.BLL.Helpers.Validation.StoreValid
{
    public class ZipValidAttribute : ValidationAttribute
    {
        private readonly string _pattern = @"^\d{5}(-\d{4})?$"; 

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; 
            }

            string zipCode = value.ToString();

            if (Regex.IsMatch(zipCode, _pattern))
            {
                return ValidationResult.Success; 
            }
            else
            {
                return new ValidationResult("Zip code is invalid");
            }
        }
    }
}
