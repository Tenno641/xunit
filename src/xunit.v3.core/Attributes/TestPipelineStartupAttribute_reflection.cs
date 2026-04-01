namespace Xunit.v3;

partial class TestPipelineStartupAttribute : ITestPipelineStartupAttribute
{ }

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="TestPipelineStartupAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class TestPipelineStartupAttribute<TPipelineStartup> : ITestPipelineStartupAttribute
{ }
