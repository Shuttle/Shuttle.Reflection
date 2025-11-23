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

            return await Task.FromResult(Guard.AgainstNull(assembly).GetTypes().Where(item => item.IsCastableTo(type) && !(item.IsInterface && item == type)).ToList());
        }

        public async Task<IEnumerable<Type>> GetTypesCastableToAsync<T>()
        {
            return await assembly.GetTypesCastableToAsync(typeof(T)).ConfigureAwait(false);
        }
    }
}