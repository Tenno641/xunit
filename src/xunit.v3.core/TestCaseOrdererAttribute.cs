using Xunit.v3;

namespace Xunit;

/// <summary>
/// Used to decorate an assembly, test collection, or test class to allow the use of a custom test case orderer.
/// </summary>
/// <param name="ordererType">The orderer type; must implement <see cref="ITestCaseOrderer"/></param>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed partial class TestCaseOrdererAttribute(Type ordererType) : Attribute, ITestOrdererAttribute
{
	/// <inheritdoc/>
	public Type OrdererType => Guard.ArgumentNotNull(ordererType);
}

/// <summary>
/// Used to decorate an assembly, test collection, or test class to allow the use of a custom test case orderer.
/// </summary>
/// <typeparam name="TOrderer">The orderer type</typeparam>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed partial class TestCaseOrdererAttribute<TOrderer> : Attribute, ITestOrdererAttribute
	where TOrderer : ITestCaseOrderer
{
	/// <inheritdoc/>
	public Type OrdererType => typeof(TOrderer);
}
