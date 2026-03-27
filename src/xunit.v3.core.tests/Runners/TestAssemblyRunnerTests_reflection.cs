using Xunit;

partial class TestAssemblyRunnerTests
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
			var runner = new TestableTestAssemblyRunner();

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
				@event => Assert.Equal(@"[TestAssemblyStart] assemblyPath = ""./test-assembly.dll"", configFileName = ""<none>""", @event),
				@event => Assert.Equal(@"[TestAssemblyStop] assemblyPath = ""./test-assembly.dll"", configFileName = ""<none>""", @event)
			);
		}
	}
}
