using Xunit;
using Xunit.v3;

partial class TestRunnerBaseTests
{
	[Collection(typeof(SpyEventListener))]
<<<<<<< HEAD
	public class EventSourceLogging
=======
	public static class EventSourceLogging
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		// Dynamic skip behavior doesn't exist at this low level, so we only test static skipping.
		// It could be simulated with a custom context, but we test the actual behavior at layers above,
		// which will de-facto test the ability of the context to permit dynamic skipping.
		public static IEnumerable<TheoryDataRow<Func<ValueTask<RunSummary>>, string>> EventSourceData()
		{
			yield return new(() => new TestableTestRunnerBase().Run(), "Passed");
			yield return new(() => new TestableTestRunnerBase() { ShouldTestRun__Result = false }.Run(), "NotRun");
			yield return new(() => new TestableTestRunnerBase().Run("Don't run me"), "Skipped");
			yield return new(() => new TestableTestRunnerBase() { RunTest__Lambda = () => Assert.Fail() }.Run(), "Failed");
		}

		[Theory(DisableDiscoveryEnumeration = true)]
		[MemberData(nameof(EventSourceData))]
<<<<<<< HEAD
		public async ValueTask LogsEvents(
=======
		public static async ValueTask LogsEvents(
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
			Func<ValueTask<RunSummary>> runner,
			string expectedResult)
		{
			var listener = new SpyEventListener();

			try
			{
				await runner();
			}
			finally
			{
				listener.Dispose();
			}

			var events = await listener.WaitForEventCount(2);
			Assert.Collection(
				events,
				@event => Assert.Equal(@"[TestStart] testName = ""test-display-name""", @event),
				@event => Assert.Equal(@$"[TestStop] testName = ""test-display-name"", result = ""{expectedResult}""", @event)
			);
		}
	}
}
