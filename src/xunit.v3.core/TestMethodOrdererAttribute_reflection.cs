using Xunit.v3;

namespace Xunit;

partial class TestMethodOrdererAttribute : ITestMethodOrdererAttribute
{ }

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="TestMethodOrdererAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class TestMethodOrdererAttribute<TOrderer> : ITestMethodOrdererAttribute
{ }
