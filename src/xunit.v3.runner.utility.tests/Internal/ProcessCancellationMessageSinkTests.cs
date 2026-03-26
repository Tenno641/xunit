using Xunit;
using Xunit.v3;

public static class ProcessCancellationMessageSinkTests
{
	[Fact]
	public static void True_NoCancellation()
	{
		var callCounter = 0;
		var process = new SpyTestProcessBase { OnCancel = _ => callCounter++ };
		var spySink = SpyMessageSink.Create(returnResult: true);
		var sink = new ProcessCancellationMessageSink(spySink, process);

		sink.OnMessage(new DiagnosticMessage("message"));

		Assert.Equal(0, callCounter);
	}

	[Fact]
	public static void False_Cancellation()
	{
		var callCounter = 0;
		var process = new SpyTestProcessBase { OnCancel = _ => callCounter++ };
		var spySink = SpyMessageSink.Create(returnResult: false);
		var sink = new ProcessCancellationMessageSink(spySink, process);

		sink.OnMessage(new DiagnosticMessage("message"));

		Assert.Equal(1, callCounter);
	}
}
