using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace Shuttle.Reflection;

public static class AssemblyStaticExtensions
{
    extension(Assembly)
    {
        public static IEnumerable<Assembly> GetRuntimeAssemblies()
        {
            var result = new List<Assembly>();
            var dependencyContext = DependencyContext.Default;

            if (dependencyContext != null)
            {
                foreach (var assemblyName in dependencyContext.RuntimeLibraries.SelectMany(library => library.GetDefaultAssemblyNames(dependencyContext)))
                {
                    try
                    {
                        result.Add(Assembly.Load(assemblyName));
                    }
                    catch (Exception)
                    {
                        // ignore
                    }
                }
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (result.All(item => item.GetName().Name != assembly.GetName().Name))
                {
                    result.Add(assembly);
                }
            }

            return result;
        }

        public static IEnumerable<Type> FindTypesCastableTo(Type type)
        {
            var result = new HashSet<Type>();

            foreach (var assembly in Assembly.GetRuntimeAssemblies())
            {
                foreach (var candidate in assembly.FindTypesCastableTo(type))
                {
                    result.Add(candidate);
                }
            }

            return result;
        }

        public static IEnumerable<Type> FindTypesCastableTo<T>()
        {
            return Assembly.FindTypesCastableTo(typeof(T));
        }
    }
}