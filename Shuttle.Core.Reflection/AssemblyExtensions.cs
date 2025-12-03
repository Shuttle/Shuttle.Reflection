using Shuttle.Core.Contract;
using System.Reflection;

namespace Shuttle.Core.Reflection;

public static class AssemblyExtensions
{
    extension(Assembly assembly)
    {
        public async Task<IEnumerable<Type>> GetTypesCastableToAsync(Type type)
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

            return await Task.FromResult(types.Where(item => item.IsCastableTo(type) && !(item.IsInterface && item == type)).ToList());
        }

        public async Task<IEnumerable<Type>> GetTypesCastableToAsync<T>()
        {
            return await assembly.GetTypesCastableToAsync(typeof(T)).ConfigureAwait(false);
        }
    }
}