# Shuttle.Core.Reflection

Provides various methods to facilitate reflection handling.

## Installation

```bash
dotnet add package Shuttle.Core.Reflection
```

## Assembly Extensions

```csharp
Task<IEnumerable<Type>> GetTypesCastableToAsync(this Assembly assembly, Type type)
Task<IEnumerable<Type>> GetTypesCastableToAsync<T>(this Assembly assembly)
```

- `GetTypesCastableToAsync`: Returns all the types in the given `assembly` that can be cast to the `type` or `typeof(T)`.

## Assembly Static Extensions

These are static extensions on the `Assembly` class.

```csharp
Task<IEnumerable<Assembly>> Assembly.GetRuntimeAssembliesAsync()
Task<IEnumerable<Type>> Assembly.GetTypesCastableToAsync(Type type)
Task<IEnumerable<Type>> Assembly.GetTypesCastableToAsync<T>()
```

- `Assembly.GetRuntimeAssembliesAsync`: Returns a combination of `DependencyContext.Default.GetRuntimeAssemblyNames(Environment.OSVersion.Platform.ToString())` and `AppDomain.CurrentDomain.GetAssemblies()`.
- `Assembly.GetTypesCastableToAsync`: Returns all the types in all assemblies returned by `Assembly.GetRuntimeAssembliesAsync()` that can be cast to the `type` or `typeof(T)`.

## Enumerable Extensions

```csharp
T? Find<T>(this IEnumerable<object> list) where T : class
IEnumerable<T> FindAll<T>(this IEnumerable<object> list) where T : class
T Get<T>(this IEnumerable<object> list) where T : class
```

- `Find<T>`: Returns the single instance of type `T` from the `list`. Throws an exception if more than one instance is found. Returns `null` if no instance is found.
- `FindAll<T>`: Returns all instances of type `T` from the `list`.
- `Get<T>`: Returns the single instance of type `T` from the `list`. Throws an exception if more than one instance is found or if no instance is found.

## Exception Extensions

```csharp
string AllMessages(this Exception ex)
bool Contains<T>(this Exception ex) where T : Exception
T? Find<T>(this Exception ex) where T : Exception
Exception TrimLeading<T>(this Exception ex) where T : Exception
```

- `AllMessages`: Traverses the exception and its inner exceptions to concatenate all messages, separated by ` / `.
- `Contains<T>`: Determines whether the exception or any of its inner exceptions are of type `T`.
- `Find<T>`: Returns the first exception of type `T` found in the exception chain.
- `TrimLeading<T>`: Removes the outer exception(s) if they are of type `T` and returns the inner exception.

## Object Extensions

```csharp
void TryDispose(this object o)
Task TryDisposeAsync(this object o)
```

- `TryDispose`: Attempts to cast the object to `IDisposable` and calls `Dispose` if successful.
- `TryDisposeAsync`: Attempts to cast the object to `IAsyncDisposable` and calls `DisposeAsync`. If `IAsyncDisposable` is not implemented, it falls back to `TryDispose`.

## Type Extensions

```csharp
void AssertDefaultConstructor(this Type type)
void AssertDefaultConstructor(this Type type, string message)
Type? FirstInterface(this Type type, Type of)
Type? GetGenericArgument(this Type type, Type generic)
bool HasDefaultConstructor(this Type type)
Type? InterfaceMatching(this Type type, string includeRegexPattern, string? excludeRegexPattern = null)
IEnumerable<Type> InterfacesCastableTo<T>(this Type type)
IEnumerable<Type> InterfacesCastableTo(this Type type, Type interfaceType)
bool IsCastableTo(this Type type, Type otherType)
Type? MatchingInterface(this Type type)
```

- `AssertDefaultConstructor`: Throws an exception if the type does not have a default constructor.
- `FirstInterface`: Returns the first interface matching the naming convention `I{TypeName}` or, if no such interface is found, the first interface that is castable to the specified type.
- `GetGenericArgument`: Returns the generic argument for the specified generic type definition.
- `HasDefaultConstructor`: Determines whether the type has a default constructor.
- `InterfaceMatching`: Returns the first interface matching the include regex and not matching the exclude regex.
- `InterfacesCastableTo`: Returns all interfaces implemented by the type that are castable to the specified type.
- `IsCastableTo`: Determines whether the type is castable to the `otherType`.
- `MatchingInterface`: Returns the interface that matches the naming convention `I{TypeName}`.
