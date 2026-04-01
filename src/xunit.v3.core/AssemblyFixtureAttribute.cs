#pragma warning disable CA1813 // This attribute is unsealed because it's an extensibility point

namespace Xunit;

/// <summary>
/// Used to decorate xUnit.net test assemblies to indicate per-assembly fixture data. An instance of
/// the fixture data is initialized before any test in the assembly are run (including
/// <see cref="IAsyncLifetime.InitializeAsync"/> if it's implemented). After all the tests in the
/// assembly have been run, it is cleaned up by calling <see cref="IAsyncDisposable.DisposeAsync"/>
/// if it's implemented, or it falls back to <see cref="IDisposable.Dispose"/> if that's implemented.
/// Assembly fixtures must have a public parameterless constructor. To gain access to the fixture data
/// from inside the test, a constructor argument should be added to the test class which exactly
/// matches the fixture type.
/// </summary>
/// <param name="assemblyFixtureType">The assembly fixture class type</param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public partial class AssemblyFixtureAttribute(Type assemblyFixtureType) : Attribute
{ }

/// <summary>
/// Used to decorate xUnit.net test assemblies to indicate per-assembly fixture data. An instance of
/// the fixture data is initialized before any test in the assembly are run (including
/// <see cref="IAsyncLifetime.InitializeAsync"/> if it's implemented). After all the tests in the
/// assembly have been run, it is cleaned up by calling <see cref="IAsyncDisposable.DisposeAsync"/>
/// if it's implemented, or it falls back to <see cref="IDisposable.Dispose"/> if that's implemented.
/// Assembly fixtures must have a public parameterless constructor. To gain access to the fixture data
/// from inside the test, a constructor argument should be added to the test class which exactly
/// matches the fixture type.
/// </summary>
/// <typeparam name="TFixture">The assembly fixture class type</typeparam>
public sealed partial class AssemblyFixtureAttribute<TFixture>
{ }
