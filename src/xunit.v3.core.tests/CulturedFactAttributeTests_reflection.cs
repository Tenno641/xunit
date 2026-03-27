using Xunit;

partial class CulturedFactAttributeTests
{
	// Native AOT reports these in the generator
	[Fact]
<<<<<<< HEAD
	public async ValueTask NoCultures()
=======
	public static async ValueTask NoCultures()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var results = await RunForResultsAsync(typeof(TestClassWithNoCultures));

		var result = Assert.Single(results);
		var failed = Assert.IsType<TestFailedWithMetadata>(result);
		Assert.Equal($"{typeof(TestClassWithNoCultures).FullName}.{nameof(TestClassWithNoCultures.TestMethod)}", failed.Test.TestDisplayName);
		Assert.Equal("Xunit.CulturedFactAttribute did not provide any cultures", failed.Messages.Single());
	}

	class TestClassWithNoCultures
	{
		[CulturedFact([])]
		public void TestMethod() { }
	}
}
