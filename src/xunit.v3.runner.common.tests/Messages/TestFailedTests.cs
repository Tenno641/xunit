using Xunit;
using Xunit.Runner.Common;
using Xunit.Sdk;

public static class TestFailedTests
{
	public static class FromException
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

#if !XUNIT_AOT  // No custom exception detection since it requires reflection

		[Fact]
		public static void CustomAssertionException()
		{
			var ex = new MyAssertionException();

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Assertion, failed.Cause);
		}

#endif  // !XUNIT_AOT

		interface IAssertionException
		{ }

		class MyAssertionException : Exception, IAssertionException
		{ }

		[Fact]
		public static void BuiltInTimeoutException()
		{
			var ex = TestTimeoutException.ForTimedOutTest(2112);

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Timeout, failed.Cause);
		}

#if !XUNIT_AOT  // No custom exception detection since it requires reflection

		[Fact]
		public static void CustomTimeoutException()
		{
			var ex = new MyTimeoutException();

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Timeout, failed.Cause);
		}

		interface ITestTimeoutException
		{ }

		class MyTimeoutException : Exception, ITestTimeoutException
		{ }

		[Fact]
		public static void TimeoutExceptionTakesPriorityOverAssertionException()
		{
			var ex = new MyMultiException();

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Timeout, failed.Cause);
		}

		class MyMultiException : Exception, IAssertionException, ITestTimeoutException
		{ }

#endif  // !XUNIT_AOT
	}
}
