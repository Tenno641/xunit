namespace Xunit;

sealed partial class AssemblyFixtureAttribute
{
	/// <summary>
	/// Gets the fixture type.
	/// </summary>
	public Type AssemblyFixtureType { get; } = assemblyFixtureType;
}

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
partial class AssemblyFixtureAttribute<TFixture> : Attribute
{
	/// <summary>
	/// Gets the fixture type.
	/// </summary>
	public Type AssemblyFixtureType => typeof(TFixture);
}
