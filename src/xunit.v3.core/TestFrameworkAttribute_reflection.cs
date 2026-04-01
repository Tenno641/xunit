using Xunit.v3;

namespace Xunit;

partial class TestFrameworkAttribute : ITestFrameworkAttribute
{ }

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="TestFrameworkAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class TestFrameworkAttribute<TFramework> : ITestFrameworkAttribute
{ }
