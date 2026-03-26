using Xunit;

public static class StringExtensionsTests
{
	public static class SplitAtOuterCommas
	{
		[Fact]
		public static void NoCommas()
		{
			var result = StringExtensions.SplitAtOuterCommas("hello");

			var first = Assert.Single(result);
			Assert.Equal("hello", first);
		}

		[Fact]
		public static void CommasOutsideSquareBrackets()
		{
			var result = StringExtensions.SplitAtOuterCommas("hello, world");

			Assert.Collection(
				result,
				part => Assert.Equal("hello", part),
				part => Assert.Equal(" world", part)
			);
		}

		[Fact]
		public static void CommasInsideSquareBrackets()
		{
			var result = StringExtensions.SplitAtOuterCommas("hello, [there, my], friend");

			Assert.Collection(
				result,
				part => Assert.Equal("hello", part),
				part => Assert.Equal(" [there, my]", part),
				part => Assert.Equal(" friend", part)
			);
		}

		[Fact]
		public static void StartingComma()
		{
			var result = StringExtensions.SplitAtOuterCommas(",hello");

			Assert.Collection(
				result,
				part => Assert.Equal(string.Empty, part),
				part => Assert.Equal("hello", part)
			);
		}

		[Fact]
		public static void EscapedCommas()
		{
			var result = StringExtensions.SplitAtOuterCommas("hello\\, [there, my], friend");

			Assert.Collection(
				result,
				part => Assert.Equal("hello\\, [there, my]", part),
				part => Assert.Equal(" friend", part)
			);
		}

		[Fact]
		public void FormatTestCaseIndex()
		{
			var zeroIndexResult = StringExtensions.FormatTestCaseIndex(3);

			Assert.Equal("_003", zeroIndexResult);
		}

		[Fact]
		public void FormatTestCaseIndex_IndexIsNullOrZero_ShouldReturnNull()
		{
			var zeroIndexResult = StringExtensions.FormatTestCaseIndex(0);
			var nullIndexResult = StringExtensions.FormatTestCaseIndex(null);

			Assert.Null(zeroIndexResult);
			Assert.Null(nullIndexResult);
		}
	}
}
