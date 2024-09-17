using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task2.BLL.Helpers.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			var dalAssembly = Assembly.Load("Task2.DAL"); 
			var bllAssembly = Assembly.Load("Task2.BLL"); 

			
			var entityTypes = dalAssembly.GetTypes().Where(t => t.IsClass && t.Namespace == "Task2.DAL.Entities");

			
			foreach (var entityType in entityTypes)
			{
				var dtoTypes = bllAssembly.GetTypes()
					.Where(t => t.IsClass && t.Namespace == $"Task2.BLL.DTOs.{entityType.Name}DTOs" && t.Name.StartsWith(entityType.Name));

				foreach (var dtoType in dtoTypes)
				{
					CreateMap(entityType, dtoType).ReverseMap();
				}
			}
		}
	}
}

