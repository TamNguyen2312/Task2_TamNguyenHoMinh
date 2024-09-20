using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.BLL.DTOs.EmployeeDTOs;

namespace Task2.BLL.Helpers.Extensions.Employees
{
    public static class EmpExtensions
    {
        public static string AutoGenerateEmpId(EmployeeCreateRequestDTO empCreateRequestDTO)
        {
            
            char firstChar = char.ToUpper(empCreateRequestDTO.Fname[0]);
            char thirdChar = char.ToUpper(empCreateRequestDTO.Lname[0]);

           
            char secondChar = !string.IsNullOrEmpty(empCreateRequestDTO.Minit) ? char.ToUpper(empCreateRequestDTO.Minit[0]) : '-';

            
            Random random = new Random();
            int fourthDigit = random.Next(1, 10);

            
            int nextFourDigits = random.Next(1000, 10000);

            
            string genderSuffix = empCreateRequestDTO.Gender.ToString() == EmpGender.Male.ToString() ? "M" : "F";

            
            return $"{firstChar}{secondChar}{thirdChar}{fourthDigit}{nextFourDigits}{genderSuffix}";
        }
    }
}
