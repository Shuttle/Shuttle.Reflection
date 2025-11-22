using System.Reflection;
using System.Text.RegularExpressions;

namespace Shuttle.Core.Reflection;

public interface IReflectionService
{
    Task<IEnumerable<Assembly>> GetMatchingAssembliesAsync(Regex regex);
    Task<IEnumerable<Assembly>> GetRuntimeAssembliesAsync();
    Task<Type?> GetTypeAsync(string typeName);
    Task<IEnumerable<Type>> GetTypesCastableToAsync(Type type);
    Task<IEnumerable<Type>> GetTypesCastableToAsync(Type type, Assembly assembly);
}