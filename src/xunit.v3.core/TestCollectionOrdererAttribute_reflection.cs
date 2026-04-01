using Xunit.v3;

namespace Xunit;

partial class TestCollectionOrdererAttribute : ITestCollectionOrdererAttribute
{ }

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="TestCollectionOrdererAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class TestCollectionOrdererAttribute<TOrderer> : ITestCollectionOrdererAttribute
{ }
