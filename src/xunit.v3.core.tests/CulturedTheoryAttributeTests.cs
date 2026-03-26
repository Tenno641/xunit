using Xunit;
using Xunit.Sdk;

public partial class CulturedTheoryAttributeTests : AcceptanceTestV3
{
	[Fact]
	public async ValueTask SingleCulture()
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
	public async ValueTask IndexedSingleCulture()
	{
#if XUNIT_AOT
		var results = await RunForResultsAsync("CulturedTheoryAttributeTests+IndexedTestClassWithSingleCulture");
#else
		var results = await RunForResultsAsync(typeof(IndexedTestClassWithSingleCulture));
#endif

		var result = Assert.Single(results);
		var passed = Assert.IsType<TestPassedWithMetadata>(result);
		Assert.Equal("CulturedTheoryAttributeTests+IndexedTestClassWithSingleCulture.TestMethod_001(_: 42)[fr-FR]", passed.Test.TestDisplayName);
	}

	[Fact]
	public async ValueTask MultipleCultures()
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

	[Fact]
	public async ValueTask IndexedMultipleCultures()
	{
#if XUNIT_AOT
		var results = await RunForResultsAsync("CulturedTheoryAttributeTests+IndexedTestClassWithMultipleCultures");
#else
		var results = await RunForResultsAsync(typeof(IndexedTestClassWithMultipleCultures));
#endif
		
		Assert.Collection(
			results.OfType<TestPassedWithMetadata>().OrderBy(passed => passed.Test.TestDisplayName),
			passed => Assert.Equal("CulturedTheoryAttributeTests+IndexedTestClassWithMultipleCultures.TestMethod_001(_: 42)[fr-FR]", passed.Test.TestDisplayName),
			passed => Assert.Equal("CulturedTheoryAttributeTests+IndexedTestClassWithMultipleCultures.TestMethod_002(_: 2112)[fr-FR]", passed.Test.TestDisplayName));
		Assert.Collection(
			results.OfType<TestFailedWithMetadata>().OrderBy(failed => failed.Test.TestDisplayName),
			failed =>
			{
				Assert.Equal("CulturedTheoryAttributeTests+IndexedTestClassWithMultipleCultures.TestMethod_001(_: 42)[en-US]", failed.Test.TestDisplayName);
				Assert.Equal(typeof(EqualException).FullName, failed.ExceptionTypes.Single());
			},
			failed =>
			{
				Assert.Equal("CulturedTheoryAttributeTests+IndexedTestClassWithMultipleCultures.TestMethod_002(_: 2112)[en-US]", failed.Test.TestDisplayName);
				Assert.Equal(typeof(EqualException).FullName, failed.ExceptionTypes.Single());
			});
	}
}
