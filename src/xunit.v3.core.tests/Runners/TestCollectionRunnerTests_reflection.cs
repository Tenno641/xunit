using Xunit;

partial class TestCollectionRunnerTests
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
			var runner = new TestableTestCollectionRunner([testCase]);

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
				@event => Assert.Equal(@"[TestCollectionStart] testCollectionName = ""test-collection-display-name""", @event),
				@event => Assert.Equal(@"[TestCollectionStop] testCollectionName = ""test-collection-display-name""", @event)
			);
		}
	}
}
