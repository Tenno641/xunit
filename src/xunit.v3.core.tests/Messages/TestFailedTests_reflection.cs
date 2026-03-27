using Xunit;
using Xunit.Sdk;
using Xunit.v3;

partial class TestFailedTests
{
	partial class FromException
	{
		[Fact]
<<<<<<< HEAD
		public void CustomAssertionException()
=======
		public static void CustomAssertionException()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var ex = new MyAssertionException();

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Assertion, failed.Cause);
		}

		interface IAssertionException
		{ }

		class MyAssertionException : Exception, IAssertionException
		{ }

		[Fact]
<<<<<<< HEAD
		public void CustomTimeoutException()
=======
		public static void CustomTimeoutException()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
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
<<<<<<< HEAD
		public void TimeoutExceptionTrumpsAssertionException()
=======
		public static void TimeoutExceptionTrumpsAssertionException()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var ex = new MyMultiException();

			var failed = TestFailed.FromException(ex, "asm-id", "coll-id", "class-id", "method-id", "case-id", "test-id", 21.12M, null, null);

			Assert.Equal(FailureCause.Timeout, failed.Cause);
		}

		class MyMultiException : Exception, IAssertionException, ITestTimeoutException
		{ }
	}
}
