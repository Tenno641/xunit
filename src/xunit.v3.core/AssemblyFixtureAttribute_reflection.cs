#pragma warning disable CA1813  // This attribute is unsealed because it's an extensibility point

using Xunit.v3;

namespace Xunit;

partial class AssemblyFixtureAttribute : IAssemblyFixtureAttribute
{
	/// <inheritdoc/>
	public Type AssemblyFixtureType => Guard.ArgumentNotNull(assemblyFixtureType);
}

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="AssemblyFixtureAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class AssemblyFixtureAttribute<TFixture>() :
	AssemblyFixtureAttribute(typeof(TFixture))
{ }
