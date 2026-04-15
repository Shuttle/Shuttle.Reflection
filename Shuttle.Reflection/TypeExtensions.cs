using System.Text.RegularExpressions;
using Shuttle.Contract;

namespace Shuttle.Reflection;

public static class TypeExtensions
{
    extension(Type type)
    {
        public void AssertDefaultConstructor()
        {
            Guard.AgainstNull(type);

            type.AssertDefaultConstructor($"Type '{type.FullName}' does not have a default constructor.");
        }

        public void AssertDefaultConstructor(string message)
        {
            if (!type.HasDefaultConstructor())
            {
                throw new NotSupportedException(message);
            }
        }

        public Type? FirstInterface(Type of)
        {
            var interfaces = type.GetInterfaces();
            var name = $"I{type.Name}";

            foreach (var i in interfaces)
            {
                if (i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }

            return interfaces.FirstOrDefault(item => item.IsCastableTo(of));
        }

        public Type? GetGenericArgument(Type generic)
        {
            return type.GetInterfaces()
                .Where(item => item.IsGenericType && item.GetGenericTypeDefinition().IsAssignableFrom(generic))
                .Select(item => item.GetGenericArguments()[0]).FirstOrDefault();
        }

        public bool HasDefaultConstructor()
        {
            return type.GetConstructor(Type.EmptyTypes) != null;
        }

        public Type? InterfaceMatching(string includeRegexPattern, string? excludeRegexPattern = null)
        {
            var includeRegex = new Regex(includeRegexPattern, RegexOptions.IgnoreCase);

            Regex? excludeRegex = null;

            if (!string.IsNullOrEmpty(excludeRegexPattern))
            {
                excludeRegex = new(excludeRegexPattern, RegexOptions.IgnoreCase);
            }

            foreach (var i in type.GetInterfaces())
            {
                var fullName = i.FullName ?? string.Empty;

                if (includeRegex.IsMatch(fullName) && (excludeRegex == null || !excludeRegex.IsMatch(fullName)))
                {
                    return i;
                }
            }

            return null;
        }

        public IEnumerable<Type> InterfacesCastableTo<T>()
        {
            return type.InterfacesCastableTo(typeof(T));
        }

        public IEnumerable<Type> InterfacesCastableTo(Type interfaceType)
        {
            Guard.AgainstNull(interfaceType);

            return type.GetInterfaces().Where(i => i.IsCastableTo(interfaceType)).ToList();
        }

        public bool IsCastableTo(Type otherType)
        {
            Guard.AgainstNull(type);
            Guard.AgainstNull(otherType);

            return type.IsGenericType && otherType.IsGenericType
                ? otherType.GetGenericTypeDefinition().IsAssignableFrom(type.GetGenericTypeDefinition())
                : otherType.IsGenericType
                    ? Type.IsCastableToGenericType(type, otherType)
                    : otherType.IsAssignableFrom(type);
        }

        private static bool IsCastableToGenericType(Type interfaceType, Type generic)
        {
            return
                interfaceType.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().IsAssignableFrom(generic));
        }

        public Type? MatchingInterface()
        {
            var name = $"I{type.Name}";

            return type.GetInterfaces()
                .Where(i => i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .Select(i => i)
                .FirstOrDefault();
        }
    }
}