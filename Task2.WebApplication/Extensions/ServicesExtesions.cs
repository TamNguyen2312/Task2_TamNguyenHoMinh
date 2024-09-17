using System.Reflection;
using Task2.BLL.Helpers.Mapper;
using Task2.DAL.Repositories;

namespace Task2.WebApplicationMVC.Extensions
{
	public static class ServicesExtesions
	{
		//Unit Of Work
		public static void AddUnitOfWork(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		//BLL Services
		public static void AddBLLServices(this IServiceCollection services)
		{
			services.Scan(scan => scan
					.FromAssemblies(Assembly.Load("Task2.BLL")) 
					.AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service"))) 
					.AsImplementedInterfaces() 
					.WithScopedLifetime());
		}

		public static void AddMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));
		}
	}
}
