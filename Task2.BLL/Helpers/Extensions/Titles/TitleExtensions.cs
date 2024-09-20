using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.BLL.Helpers.Extensions.Titles
{
	public class TitleExtensions
	{
		public static string GenerateTitleId(string type)
		{
		
			string prefix = type.ToString().Substring(0, 2).ToUpper();

			Random random = new Random();
			int randomNumber = random.Next(1000, 10000);

			return $"{prefix}{randomNumber}";
		}
	}
}
