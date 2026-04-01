using Xunit.v3;

namespace Xunit;

/// <summary>
/// Used to decorate an assembly, test collection, or test class to allow the use of a custom test method orderer.
/// </summary>
/// <param name="ordererType">The orderer type; must implement <see cref="ITestMethodOrderer"/></param>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed partial class TestMethodOrdererAttribute(Type ordererType) : Attribute, ITestOrdererAttribute
{
	/// <inheritdoc/>
	public Type OrdererType => Guard.ArgumentNotNull(ordererType);
}

/// <summary>
/// Used to decorate an assembly, test collection, or test class to allow the use of a custom test method orderer.
/// </summary>
/// <typeparam name="TOrderer">The orderer type</typeparam>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed partial class TestMethodOrdererAttribute<TOrderer> : Attribute, ITestOrdererAttribute
	where TOrderer : ITestMethodOrderer
{
	/// <inheritdoc/>
	public Type OrdererType => typeof(TOrderer);
}
