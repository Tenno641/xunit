using Xunit;
using Xunit.Sdk;

public static class IAssemblyMetadataExtensionsTests
{
	public static class SimpleAssemblyName
	{
		[Fact]
		public static void GuardClauses()
		{
			Assert.Throws<ArgumentNullException>("assemblyMetadata", () => IAssemblyMetadataExtensions.SimpleAssemblyName(null!));

			var metadata = TestData.AssemblyMetadata(assemblyName: null!);
			Assert.Throws<ArgumentNullException>("assemblyMetadata.AssemblyName", () => IAssemblyMetadataExtensions.SimpleAssemblyName(metadata));
		}

		[Fact]
		public static void ReturnsSimpleName()
		{
			var metadata = TestData.AssemblyMetadata(assemblyName: "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

			var result = IAssemblyMetadataExtensions.SimpleAssemblyName(metadata);

			Assert.Equal("mscorlib", result);
		}
	}
}
