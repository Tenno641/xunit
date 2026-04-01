using Xunit.v3;

namespace Xunit;

/// <summary>
/// Used to decorate an assembly or test collection to allow the use of a custom test class orderer.
/// </summary>
/// <param name="ordererType">The orderer type; must implement <see cref="ITestClassOrderer"/></param>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed partial class TestClassOrdererAttribute(Type ordererType) : Attribute, ITestOrdererAttribute
{
	/// <inheritdoc/>
	public Type OrdererType => Guard.ArgumentNotNull(ordererType);
}

/// <summary>
/// Used to decorate an assembly or test collection to allow the use of a custom test class orderer.
/// </summary>
/// <typeparam name="TOrderer">The orderer type</typeparam>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed partial class TestClassOrdererAttribute<TOrderer> : Attribute, ITestOrdererAttribute
	where TOrderer : ITestClassOrderer
{
	/// <inheritdoc/>
	public Type OrdererType => typeof(TOrderer);
}
