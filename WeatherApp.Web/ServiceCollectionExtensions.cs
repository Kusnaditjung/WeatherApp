using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.Web
{
	[ExcludeFromCodeCoverage]
    internal static class ServiceCollectionExtensions
    {
		public static IServiceCollection AddServicesFromLocalDllContainers(this IServiceCollection serviceCollection)
		{
			IEnumerable<Assembly> localAssemblies = GetAssembliesFromLocalDlls();
			IEnumerable<IServiceCollection> serviceContainers = GetLocalAssemblyContainer(localAssemblies);

			foreach (IServiceCollection serviceContainer in serviceContainers)
			{
				foreach (ServiceDescriptor serviceDescription in serviceContainer)
				{
					serviceCollection.Add(serviceDescription);
				}
			}

			return serviceCollection;
		}

		private static IEnumerable<IServiceCollection> GetLocalAssemblyContainer(IEnumerable<Assembly> localAssemblies)
		{
			return localAssemblies
				.SelectMany(assembly => assembly.GetTypes())
				.Where(assemblyType => typeof(IServiceCollection).IsAssignableFrom(assemblyType))
				.Select(assemblyType => Activator.CreateInstance(assemblyType) as IServiceCollection);
		}

		private static IEnumerable<Assembly> GetAssembliesFromLocalDlls()
		{
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			foreach (string dll in Directory.GetFiles(path, "*.dll"))
			{
				yield return Assembly.LoadFile(dll);
			}
		}
	}
}
