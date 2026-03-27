using Xunit;
using Xunit.Sdk;

public static class RangeAssertsTests
{
	public static class InRange
	{
		[CulturedFact(["en-US", "fr-FR"])]
<<<<<<< HEAD
		public void DoubleNotWithinRange()
=======
		public static void DoubleNotWithinRange()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var ex = Record.Exception(() => Assert.InRange(1.50, .75, 1.25));

			Assert.IsType<InRangeException>(ex);
			Assert.Equal(
				"Assert.InRange() Failure: Value not in range" + Environment.NewLine +
				$"Range:  ({0.75:G17} - {1.25:G17})" + Environment.NewLine +
				$"Actual: {1.5:G17}",
				ex.Message
			);
		}

		[Fact]
		public static void DoubleValueWithinRange()
		{
			Assert.InRange(1.0, .75, 1.25);
		}

		[Fact]
		public static void IntNotWithinRangeWithZeroActual()
		{
			var ex = Record.Exception(() => Assert.InRange(0, 1, 2));

			Assert.IsType<InRangeException>(ex);
			Assert.Equal(
				"Assert.InRange() Failure: Value not in range" + Environment.NewLine +
				"Range:  (1 - 2)" + Environment.NewLine +
				"Actual: 0",
				ex.Message
			);
		}

		[Fact]
		public static void IntNotWithinRangeWithZeroMinimum()
		{
			var ex = Record.Exception(() => Assert.InRange(2, 0, 1));

			Assert.IsType<InRangeException>(ex);
			Assert.Equal(
				"Assert.InRange() Failure: Value not in range" + Environment.NewLine +
				"Range:  (0 - 1)" + Environment.NewLine +
				"Actual: 2",
				ex.Message
			);
		}

		[Fact]
		public static void IntValueWithinRange()
		{
			Assert.InRange(2, 1, 3);
		}

		[Fact]
		public static void StringNotWithinRange()
		{
			var ex = Record.Exception(() => Assert.InRange("adam", "bob", "scott"));

			Assert.IsType<InRangeException>(ex);
			Assert.Equal(
				"Assert.InRange() Failure: Value not in range" + Environment.NewLine +
				"Range:  (\"bob\" - \"scott\")" + Environment.NewLine +
				"Actual: \"adam\"",
				ex.Message
			);
		}

		[Fact]
		public static void StringValueWithinRange()
		{
			Assert.InRange("bob", "adam", "scott");
		}
	}

	public static class InRange_WithComparer
	{
		[Fact]
		public static void DoubleValueWithinRange()
		{
			Assert.InRange(400.0, .75, 1.25, new DoubleComparer(-1));
		}

		[CulturedFact(["en-US", "fr-FR"])]
<<<<<<< HEAD
		public void DoubleValueNotWithinRange()
=======
		public static void DoubleValueNotWithinRange()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var ex = Record.Exception(() => Assert.InRange(1.0, .75, 1.25, new DoubleComparer(1)));

			Assert.IsType<InRangeException>(ex);
			Assert.Equal(
				"Assert.InRange() Failure: Value not in range" + Environment.NewLine +
				$"Range:  ({0.75:G17} - {1.25:G17})" + Environment.NewLine +
				$"Actual: {1:G17}",
				ex.Message
			);
		}
	}

	public static class NotInRange
	{
		[Fact]
		public static void DoubleNotWithinRange()
		{
			Assert.NotInRange(1.50, .75, 1.25);
		}

		[Fact]
		public static void DoubleWithinRange()
		{
			var ex = Record.Exception(() => Assert.NotInRange(1.0, .75, 1.25));

			Assert.IsType<NotInRangeException>(ex);
			Assert.Equal(
				"Assert.NotInRange() Failure: Value in range" + Environment.NewLine +
				"Range:  (0.75 - 1.25)" + Environment.NewLine +
				"Actual: 1",
				ex.Message
			);
		}

		[Fact]
		public static void IntNotWithinRange()
		{
			Assert.NotInRange(1, 2, 3);
		}

		[Fact]
		public static void IntWithinRange()
		{
			var ex = Record.Exception(() => Assert.NotInRange(2, 1, 3));

			Assert.IsType<NotInRangeException>(ex);
			Assert.Equal(
				"Assert.NotInRange() Failure: Value in range" + Environment.NewLine +
				"Range:  (1 - 3)" + Environment.NewLine +
				"Actual: 2",
				ex.Message
			);
		}

		[Fact]
		public static void StringNotWithNotInRange()
		{
			Assert.NotInRange("adam", "bob", "scott");
		}

		[Fact]
		public static void StringWithNotInRange()
		{
			var ex = Record.Exception(() => Assert.NotInRange("bob", "adam", "scott"));

			Assert.IsType<NotInRangeException>(ex);
			Assert.Equal(
				"Assert.NotInRange() Failure: Value in range" + Environment.NewLine +
				"Range:  (\"adam\" - \"scott\")" + Environment.NewLine +
				"Actual: \"bob\"",
				ex.Message
			);
		}
	}

	public static class NotInRange_WithComparer
	{
		[Fact]
		public static void DoubleValueWithinRange()
		{
			var ex = Record.Exception(() => Assert.NotInRange(400.0, .75, 1.25, new DoubleComparer(-1)));

			Assert.IsType<NotInRangeException>(ex);
			Assert.Equal(
				"Assert.NotInRange() Failure: Value in range" + Environment.NewLine +
				"Range:  (0.75 - 1.25)" + Environment.NewLine +
				"Actual: 400",
				ex.Message
			);
		}

		[Fact]
		public static void DoubleValueNotWithinRange()
		{
			Assert.NotInRange(1.0, .75, 1.25, new DoubleComparer(1));
		}
	}

	class DoubleComparer(int returnValue) :
		IComparer<double>
	{
		public int Compare(
			double x,
			double y) =>
				returnValue;
	}
}
