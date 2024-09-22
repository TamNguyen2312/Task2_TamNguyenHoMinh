using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.BLL.Helpers.Extensions.Exceptions
{
	public static class ExceptionExtensions
	{
		public static List<string> GetExceptionMessages(Exception ex)
		{
			var messages = new List<string>();
			var currentEx = ex;

			// Duyệt qua tất cả các inner exceptions và thêm message của từng exception vào list
			while (currentEx != null)
			{
				messages.Add(currentEx.Message);
				currentEx = currentEx.InnerException;
			}

			return messages;
		}
	}
}
