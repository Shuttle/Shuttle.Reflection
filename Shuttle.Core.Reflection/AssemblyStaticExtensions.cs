using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace Shuttle.Core.Reflection;

public static class AssemblyStaticExtensions
{
    extension(Assembly)
    {
        public static async Task<IEnumerable<Assembly>> GetRuntimeAssembliesAsync()
        {
            var result = new List<Assembly>();
            var dependencyContext = DependencyContext.Default;

            if (dependencyContext != null)
            {
                result.AddRange(dependencyContext.GetRuntimeAssemblyNames(Environment.OSVersion.Platform.ToString()).Select(Assembly.Load));
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (result.All(item => item.GetName().Equals(assembly.GetName())))
                {
                    result.Add(assembly);
                }
            }

            return await Task.FromResult(result);
        }

        public static async Task<IEnumerable<Type>> GetTypesCastableToAsync(Type type)
        {
            var result = new List<Type>();

            var assemblies = await GetRuntimeAssembliesAsync().ConfigureAwait(false);

            foreach (var assembly in assemblies)
            {
                var types = await assembly.GetTypesCastableToAsync(type).ConfigureAwait(false);

                types.Where(candidate => result.Find(existing => existing == candidate) == null)
                    .ToList()
                    .ForEach(add => result.Add(add));
            }

            return result;
        }

        public static async Task<IEnumerable<Type>> GetTypesCastableToAsync<T>()
        {
            return await GetTypesCastableToAsync(typeof(T)).ConfigureAwait(false);
        }
    }
}