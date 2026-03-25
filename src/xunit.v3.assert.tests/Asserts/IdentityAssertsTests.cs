using Xunit;
using Xunit.Sdk;

public static class IdentityAssertsTests
{
	public static class NotSame
	{
		[Fact]
		public static void Identical()
		{
			var actual = new object();

			var ex = Record.Exception(() => Assert.NotSame(actual, actual));

			Assert.IsType<NotSameException>(ex);
			Assert.Equal("Assert.NotSame() Failure: Values are the same instance", ex.Message);
		}

		[Fact]
		public static void NotIdentical()
		{
			Assert.NotSame("bob", "jim");
		}
	}

	public static class Same
	{
		[Fact]
		public static void Identical()
		{
			var actual = new object();

			Assert.Same(actual, actual);
		}

		[Fact]
		public static void NotIdentical()
		{
			var ex = Record.Exception(() => Assert.Same("bob", "jim"));

			Assert.IsType<SameException>(ex);
			Assert.Equal(
				"Assert.Same() Failure: Values are not the same instance" + Environment.NewLine +
				"Expected: \"bob\"" + Environment.NewLine +
				"Actual:   \"jim\"",
				ex.Message
			);
		}

		[Fact]
		public static void EqualValueTypeValuesAreNotSameBecauseOfBoxing()
		{
#pragma warning disable xUnit2005 // Do not use identity check on value type
			Assert.Throws<SameException>(() => Assert.Same(0, 0));
#pragma warning restore xUnit2005 // Do not use identity check on value type
		}
	}
}
