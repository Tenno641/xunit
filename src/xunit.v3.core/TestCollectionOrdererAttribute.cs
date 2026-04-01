using Xunit.v3;

namespace Xunit;

/// <summary>
/// Used to decorate an assembly to allow the use of a custom test collection orderer.
/// </summary>
/// <param name="ordererType">The orderer type; must implement <see cref="ITestCollectionOrderer"/></param>
[AttributeUsage(AttributeTargets.Assembly, Inherited = true, AllowMultiple = false)]
public sealed partial class TestCollectionOrdererAttribute(Type ordererType) : Attribute, ITestOrdererAttribute
{
	/// <inheritdoc/>
	public Type OrdererType => Guard.ArgumentNotNull(ordererType);
}

/// <summary>
/// Used to decorate an assembly to allow the use of a custom test collection orderer.
/// </summary>
/// <typeparam name="TOrderer">The orderer type</typeparam>
[AttributeUsage(AttributeTargets.Assembly, Inherited = true, AllowMultiple = false)]
public sealed partial class TestCollectionOrdererAttribute<TOrderer> : Attribute, ITestOrdererAttribute
	where TOrderer : ITestCollectionOrderer
{
	/// <inheritdoc/>
	public Type OrdererType => typeof(TOrderer);
}
