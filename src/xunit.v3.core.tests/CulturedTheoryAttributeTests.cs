using Xunit;
using Xunit.Sdk;

public partial class CulturedTheoryAttributeTests : AcceptanceTestV3
{
	[Fact]
<<<<<<< HEAD
	public async ValueTask SingleCulture()
=======
	public static async ValueTask SingleCulture()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
#if XUNIT_AOT
		var results = await RunForResultsAsync("CulturedTheoryAttributeTests+TestClassWithSingleCulture");
#else
		var results = await RunForResultsAsync(typeof(TestClassWithSingleCulture));
#endif

		var result = Assert.Single(results);
		var passed = Assert.IsType<TestPassedWithMetadata>(result);
		Assert.Equal("CulturedTheoryAttributeTests+TestClassWithSingleCulture.TestMethod(_: 42)[fr-FR]", passed.Test.TestDisplayName);
	}

	[Fact]
	public static async ValueTask MultipleCultures()
	{
#if XUNIT_AOT
		var results = await RunForResultsAsync("CulturedTheoryAttributeTests+TestClassWithMultipleCultures");
#else
		var results = await RunForResultsAsync(typeof(TestClassWithMultipleCultures));
#endif

		Assert.Collection(
			results.OfType<TestPassedWithMetadata>().OrderBy(passed => passed.Test.TestDisplayName),
			passed => Assert.Equal("CulturedTheoryAttributeTests+TestClassWithMultipleCultures.TestMethod(_: 2112)[fr-FR]", passed.Test.TestDisplayName),
			passed => Assert.Equal("CulturedTheoryAttributeTests+TestClassWithMultipleCultures.TestMethod(_: 42)[fr-FR]", passed.Test.TestDisplayName)
		);
		Assert.Collection(
			results.OfType<TestFailedWithMetadata>().OrderBy(failed => failed.Test.TestDisplayName),
			failed =>
			{
				Assert.Equal("CulturedTheoryAttributeTests+TestClassWithMultipleCultures.TestMethod(_: 2112)[en-US]", failed.Test.TestDisplayName);
				Assert.Equal(typeof(EqualException).FullName, failed.ExceptionTypes.Single());
			},
			failed =>
			{
				Assert.Equal("CulturedTheoryAttributeTests+TestClassWithMultipleCultures.TestMethod(_: 42)[en-US]", failed.Test.TestDisplayName);
				Assert.Equal(typeof(EqualException).FullName, failed.ExceptionTypes.Single());
			}
		);
	}
}
