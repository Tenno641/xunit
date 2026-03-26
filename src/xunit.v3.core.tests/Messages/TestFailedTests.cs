using Xunit;
using Xunit.Sdk;
using Xunit.v3;

public static partial class TestFailedTests
{
	public static class Cause
	{
		[Fact]
		public static void GuardClause()
		{
			var ex = Record.Exception(() => new TestFailed
			{
				Cause = (FailureCause)2112,

				AssemblyUniqueID = "",
				ExceptionParentIndices = [],
				ExceptionTypes = [],
				ExecutionTime = 0,
				FinishTime = DateTimeOffset.UtcNow,
				Messages = [],
				Output = "",
				StackTraces = [],
				TestCaseUniqueID = "",
				TestClassUniqueID = "",
				TestCollectionUniqueID = "",
				TestMethodUniqueID = "",
				TestUniqueID = "",
				Warnings = [],
			});

			var argEx = Assert.IsType<ArgumentException>(ex);
			Assert.Equal("Cause", argEx.ParamName);
			Assert.StartsWith($"Enum value 2112 not in valid set: [Assertion, Exception, Other, Timeout]", argEx.Message);
		}
	}

	public static partial class FromException
	{
		[Fact]
		public static void NonAssertionException()
		{
			var ex = new DivideByZeroException();

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Exception, failed.Cause);
		}

		[Fact]
		public static void BuiltInAssertionException()
		{
			var ex = EqualException.ForMismatchedValues("42", "2112");

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Assertion, failed.Cause);
		}

		[Fact]
		public static void BuiltInTimeoutException()
		{
			var ex = TestTimeoutException.ForTimedOutTest(2112);

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Timeout, failed.Cause);
		}
	}
}
