using Xunit;
using Xunit.Sdk;
using Xunit.v3;

<<<<<<< HEAD
public class CoreTestCaseRunnerTests
{
	public class InvokeHandlers
	{
		[Fact]
		public async ValueTask RunsPreAndPostInvokeByDefault()
=======
public static class CoreTestCaseRunnerTests
{
	public static class InvokeHandlers
	{
		[Fact]
		public static async ValueTask RunsPreAndPostInvokeByDefault()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var operations = new List<string>();
			var testCase = Mocks.CoreTestCase(
				preInvoke: () => operations.Add("PreInvoke()"),
				postInvoke: () => operations.Add("PostInvoke()")
			);
			var runner = new TestableCoreTestCaseRunner(testCase);

			var result = await runner.RunAsync();

			Assert.Equal(1, result.Total);
			Assert.Equal(0, result.Failed);
			Assert.Equal(0, result.Skipped);
			Assert.Equal(0, result.NotRun);
			Assert.Collection(
				operations,
				op => Assert.Equal("PreInvoke()", op),
				op => Assert.Equal("PostInvoke()", op)
			);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask PreInvokeFails_SkipsPostInvoke()
=======
		public static async ValueTask PreInvokeFails_SkipsPostInvoke()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var operations = new List<string>();
			var testCase = Mocks.CoreTestCase(
				preInvoke: () => { operations.Add("PreInvoke()"); throw new DivideByZeroException(); },
				postInvoke: () => operations.Add("PostInvoke()")
			);
			var runner = new TestableCoreTestCaseRunner(testCase);

			var result = await runner.RunAsync();

			Assert.Equal(1, result.Total);
			Assert.Equal(1, result.Failed);
			Assert.Equal(0, result.Skipped);
			Assert.Equal(0, result.NotRun);
			Assert.Equal("PreInvoke()", Assert.Single(operations));
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask AggregatorContainsException_SkipsPreAndPostInvoke()
=======
		public static async ValueTask AggregatorContainsException_SkipsPreAndPostInvoke()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var operations = new List<string>();
			var testCase = Mocks.CoreTestCase(
				preInvoke: () => operations.Add("PreInvoke()"),
				postInvoke: () => operations.Add("PostInvoke()")
			);
			var runner = new TestableCoreTestCaseRunner(testCase);
			runner.Aggregator.Add(new DivideByZeroException());

			var result = await runner.RunAsync();

			Assert.Equal(1, result.Total);
			Assert.Equal(1, result.Failed);
			Assert.Equal(0, result.Skipped);
			Assert.Equal(0, result.NotRun);
			Assert.Empty(operations);
		}
	}

	class TestableCoreTestCaseRunner(ICoreTestCase testCase) :
		CoreTestCaseRunner<TestableCoreTestCaseRunner.TestableContext, ICoreTestCase, ICoreTest>
	{
		public readonly ExceptionAggregator Aggregator = new();
		public readonly CancellationTokenSource CancellationTokenSource = new();
		public readonly SpyMessageBus MessageBus = new();

		public async ValueTask<RunSummary> RunAsync()
		{
			await using var ctxt = new TestableContext(
				testCase,
				[Mocks.CoreTest(testCase: testCase)],
				ExplicitOption.Off,
				MessageBus,
				Aggregator,
				testCase.TestCaseDisplayName,
				testCase.SkipReason,
				CancellationTokenSource
			);
			await ctxt.InitializeAsync();

			return await Run(ctxt);
		}

		public class TestableContext(
			ICoreTestCase testCase,
			IReadOnlyCollection<ICoreTest> tests,
			ExplicitOption explicitOption,
			IMessageBus messageBus,
			ExceptionAggregator aggregator,
			string displayName,
			string? skipReason,
			CancellationTokenSource cancellationTokenSource) :
				CoreTestCaseRunnerContext<ICoreTestCase, ICoreTest>(testCase, tests, explicitOption, messageBus, aggregator, displayName, skipReason, cancellationTokenSource)
		{
			public override ValueTask<RunSummary> RunTest(ICoreTest test) =>
				new(new RunSummary { Total = 1 });
		}
	}
}
