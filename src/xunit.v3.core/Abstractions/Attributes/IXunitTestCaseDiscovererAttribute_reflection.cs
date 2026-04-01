namespace Xunit.v3;

/// <summary>
/// An attribute used to decorate classes which implement <see cref="IFactAttribute"/>,
/// to indicate how test cases should be discovered.
/// </summary>
/// <remarks>Test case discoverer attributes are only valid at the class level, and only a
/// single instance is allowed.</remarks>
public interface IXunitTestCaseDiscovererAttribute
{
	/// <summary>
	/// Gets the type of the test case discoverer.
	/// </summary>
	Type Type { get; }
}
