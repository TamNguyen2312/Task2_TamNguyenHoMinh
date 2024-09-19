using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.BLL.Helpers.Extensions.Stores
{
    public class StoreExtensions
    {
        public static string AutoGenerateStoreId()
        {
            Random random = new Random();
            int number = random.Next(1000, 10000);
            return number.ToString();
        }
    }
}
