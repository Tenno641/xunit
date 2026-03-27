using Xunit;

partial class TestClassRunnerTests
{
	[Collection(typeof(SpyEventListener))]
<<<<<<< HEAD
	public class EventSourceLogging
	{
		[Fact]
		public async ValueTask LogsEvents()
=======
	public static class EventSourceLogging
	{
		[Fact]
		public static async ValueTask LogsEvents()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var listener = new SpyEventListener();
			var testCase = Mocks.TestCase();
			var runner = new TestableTestClassRunner([testCase]);

			try
			{
				await runner.RunAsync();
			}
			finally
			{
				listener.Dispose();
			}

			var events = await listener.WaitForEventCount(2);
			Assert.Collection(
				events,
				@event => Assert.Equal(@"[TestClassStart] testClassName = ""test-class-name""", @event),
				@event => Assert.Equal(@"[TestClassStop] testClassName = ""test-class-name""", @event)
			);
		}
	}
}
