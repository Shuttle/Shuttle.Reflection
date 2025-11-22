using System.Reflection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Reflection;

public static class ReflectionServiceExtensions
{
    extension(IReflectionService service)
    {
        public async Task<IEnumerable<Assembly>> GetAssembliesAsync()
        {
            return await service.GetMatchingAssembliesAsync(new(".*")).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Assembly>> GetMatchingAssembliesAsync(string regex)
        {
            return await Guard.AgainstNull(service).GetMatchingAssembliesAsync(new(Guard.AgainstEmpty(regex))).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Type>> GetTypesCastableToAsync<T>()
        {
            return await Guard.AgainstNull(service).GetTypesCastableToAsync(typeof(T)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Type>> GetTypesCastableToAsync<T>(Assembly assembly)
        {
            return await Guard.AgainstNull(service).GetTypesCastableToAsync(typeof(T), assembly).ConfigureAwait(false);
        }
    }
}