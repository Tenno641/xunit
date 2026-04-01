using Xunit.v3;

namespace Xunit;

/// <summary>
/// Used to decorate an assembly to allow the use of a custom test framework.
/// </summary>
/// <param name="frameworkType">The framework type; must implement <see cref="ITestFramework"/></param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
public sealed partial class TestFrameworkAttribute(Type frameworkType) : Attribute
{
	/// <summary>
	/// Gets the framework type
	/// </summary>
	public Type FrameworkType => Guard.ArgumentNotNull(frameworkType);
}

/// <summary>
/// Used to decorate an assembly to allow the use of a custom test framework.
/// </summary>
/// <typeparam name="TFramework">The framework type</typeparam>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
public sealed partial class TestFrameworkAttribute<TFramework> : Attribute
	where TFramework : ITestFramework
{
	/// <summary>
	/// Gets the framework type
	/// </summary>
	public Type FrameworkType => typeof(TFramework);
}
