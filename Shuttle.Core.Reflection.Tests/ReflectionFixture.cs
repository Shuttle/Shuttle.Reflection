using System.Reflection;
using NUnit.Framework;

namespace Shuttle.Core.Reflection.Tests;

[TestFixture]
public class ReflectionFixture
{
    [Test]
    public async Task Should_be_able_to_get_runtime_assemblies_async()
    {
        Assert.That((await Assembly.GetRuntimeAssembliesAsync()).Count(), Is.GreaterThan(0));
    }

    [Test]
    public async Task Should_be_able_to_get_types_async()
    {
        Assert.That((await Assembly.FindTypesCastableTo<SomeClass>()).Count(), Is.EqualTo(1));
        Assert.That((await Assembly.FindTypesCastableTo<ISomeClass>()).Count(), Is.EqualTo(1));
    }
}