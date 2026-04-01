using Xunit.v3;

namespace Xunit;

partial class TestClassOrdererAttribute : ITestClassOrdererAttribute
{ }

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="TestClassOrdererAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class TestClassOrdererAttribute<TOrderer> : ITestClassOrdererAttribute
{ }
