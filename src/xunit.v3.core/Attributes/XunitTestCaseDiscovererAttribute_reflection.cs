namespace Xunit.v3;

/// <summary>
/// An attribute used to decorate classes which implement <see cref="IFactAttribute"/>,
/// to indicate how test cases should be discovered.
/// </summary>
/// <param name="type">The type of the discoverer; must implement <see cref="IXunitTestCaseDiscoverer"/>.</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class XunitTestCaseDiscovererAttribute(Type type) : Attribute, IXunitTestCaseDiscovererAttribute
{
	/// <inheritdoc/>
	public Type Type => Guard.ArgumentNotNull(type);
}

/// <summary>
/// An attribute used to decorate classes which implement <see cref="IFactAttribute"/>,
/// to indicate how test cases should be discovered.
/// </summary>
/// <typeparam name="TDiscoverer">The type of the discoverer</typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class XunitTestCaseDiscovererAttribute<TDiscoverer> : Attribute, IXunitTestCaseDiscovererAttribute
	where TDiscoverer : IXunitTestCaseDiscoverer
{
	/// <inheritdoc/>
	public Type Type => typeof(TDiscoverer);
}
