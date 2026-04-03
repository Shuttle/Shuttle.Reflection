using Shuttle.Core.Contract;
using System.Reflection;

namespace Shuttle.Core.Reflection;

public static class AssemblyExtensions
{
    extension(Assembly assembly)
    {
        public IEnumerable<Type> FindTypesCastableTo(Type type)
        {
            Guard.AgainstNull(type);

            var types = new List<Type>();

            try
            {
                types.AddRange(Guard.AgainstNull(assembly).GetTypes());
            }
            catch (ReflectionTypeLoadException ex)
            {
                types.AddRange(ex.Types.Where(t => t != null)!);
            }

            return types.Where(item => item.IsCastableTo(type) && !(item.IsInterface && item == type)).ToList();
        }

        public IEnumerable<Type> FindTypesCastableTo<T>()
        {
            return assembly.FindTypesCastableTo(typeof(T));
        }
    }
}