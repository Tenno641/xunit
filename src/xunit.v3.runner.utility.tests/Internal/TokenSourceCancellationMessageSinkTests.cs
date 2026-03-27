using Xunit;
using Xunit.v3;

public static class TokenSourceCancellationMessageSinkTests
{
	[Fact]
	public static void True_NoCancellation()
	{
		var cts = new CancellationTokenSource();
		var spySink = SpyMessageSink.Create(returnResult: true);
		var sink = new TokenSourceCancellationMessageSink(spySink, cts);

		sink.OnMessage(new DiagnosticMessage("message"));

		Assert.False(cts.IsCancellationRequested);
	}

	[Fact]
	public static void False_Cancellation()
	{
		var cts = new CancellationTokenSource();
		var spySink = SpyMessageSink.Create(returnResult: false);
		var sink = new TokenSourceCancellationMessageSink(spySink, cts);

		sink.OnMessage(new DiagnosticMessage("message"));

		Assert.True(cts.IsCancellationRequested);
	}
}
