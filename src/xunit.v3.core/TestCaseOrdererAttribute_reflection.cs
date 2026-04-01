using Xunit.v3;

namespace Xunit;

partial class TestCaseOrdererAttribute : ITestCaseOrdererAttribute
{ }

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="TestCaseOrdererAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class TestCaseOrdererAttribute<TOrderer> : ITestCaseOrdererAttribute
{ }
