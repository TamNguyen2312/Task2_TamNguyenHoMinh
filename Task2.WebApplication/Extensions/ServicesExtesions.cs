using System.Reflection;
using Task2.DAL.Repositories;

namespace Task2.WebApplicationMVC.Extensions
{
	public static class ServicesExtesions
	{
		//Unit Of Work
		public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}

		//BLL Services
		public static IServiceCollection AddBLLServices(this IServiceCollection services)
		{
			services.Scan(scan => scan
					.FromAssemblies(Assembly.Load("BLL")) 
					.AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service"))) 
					.AsImplementedInterfaces() 
					.WithScopedLifetime());

			return services;
		}
	}
}
