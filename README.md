# Shuttle.Core.Reflection

Provides various methods to facilitate reflection handling.

## Instance Extensions

``` c#
Task<IEnumerable<Type>> assembly.GetTypesCastableToAsync(Type type)
Task<IEnumerable<Type>> assembly.GetTypesCastableToAsync<T>();
```

Returns all the types in the given `assembly` that can be cast to the `type` or `typeof(T)`.

## Static Extensions

``` c#
Task<IEnumerable<Assembly>> Assembly.GetRuntimeAssembliesAsync()
```

Returns a combination of `DependencyContext.Default.GetRuntimeAssemblyNames(Environment.OSVersion.Platform.ToString())` and `AppDomain.CurrentDomain.GetAssemblies()`.

``` c#
Task<IEnumerable<Type>> Assembly.GetTypesCastableToAsync<T>();
Task<IEnumerable<Type>> Assembly.GetTypesCastableToAsync(Type type);
```

Returns all the types in all assemblies returned by `Assembly.GetRuntimeAssembliesAsync()` that can be cast to the `type` or `typeof(T)`.

