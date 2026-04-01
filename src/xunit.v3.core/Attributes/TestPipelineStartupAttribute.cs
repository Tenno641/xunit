namespace Xunit.v3;

/// <summary>
/// Used to decorate xUnit.net test assemblies to indicate that the developer wishes to have code
/// that runs during the test pipeline startup and shutdown (including both discovery and execution).
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
public sealed partial class TestPipelineStartupAttribute(Type testPipelineStartupType) : Attribute
{
	/// <summary>
	/// Gets the test pipeline startup type
	/// </summary>
	public Type TestPipelineStartupType => Guard.ArgumentNotNull(testPipelineStartupType);
}

/// <summary>
/// Used to decorate xUnit.net test assemblies to indicate that the developer wishes to have code
/// that runs during the test pipeline startup and shutdown (including both discovery and execution).
/// </summary>
/// <typeparam name="TPipelineStartup">The test pipeline startup type</typeparam>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
public sealed partial class TestPipelineStartupAttribute<TPipelineStartup> : Attribute
	where TPipelineStartup : ITestPipelineStartup
{
	/// <summary>
	/// Gets the test pipeline startup type
	/// </summary>
	public Type TestPipelineStartupType => typeof(TPipelineStartup);
}
