using Xunit;
using Xunit.Sdk;
using Xunit.v3;

partial class FixtureMappingManagerTests
{
	// Native AOT reports these in the generator
	[Fact]
<<<<<<< HEAD
	public async ValueTask MoreThanOneConstructorThrows()
=======
	public static async ValueTask MoreThanOneConstructorThrows()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var manager = new TestableFixtureMappingManager();

		var ex = await Record.ExceptionAsync(async () => await manager.InitializeAsync(typeof(int)));

		Assert.IsType<TestPipelineException>(ex);
		Assert.Equal("Testable fixture type 'System.Int32' may only define a single public constructor.", ex.Message);
	}

	class TestableFixtureMappingManager : FixtureMappingManager
	{
		public TestableFixtureMappingManager(FixtureMappingManager parent) :
			base("Testable", parent)
		{ }

		public TestableFixtureMappingManager(params object[] cachedFixtureValues) :
			base("Testable", cachedFixtureValues)
		{ }
	}
}
