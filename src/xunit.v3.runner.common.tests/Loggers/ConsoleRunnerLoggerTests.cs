using Xunit;
using Xunit.Runner.Common;

public static class ConsoleRunnerLoggerTests
{
	public static class LogWithAnsiColors
	{
		[Fact]
		public static void LogError_InRed()
		{
			var writer = new StringWriter();
			var sut = new ConsoleRunnerLogger(true, true, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.LogError("This is an error message");

			Assert.Equal("\u001b[91mThis is an error message" + Environment.NewLine + "\u001b[0m", writer.ToString());
		}

		[Fact]
		public static void LogImportantMessage_InGray()
		{
			var writer = new StringWriter();
			var sut = new ConsoleRunnerLogger(true, true, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.LogImportantMessage("This is an important message");

			Assert.Equal("\u001b[37mThis is an important message" + Environment.NewLine + "\u001b[0m", writer.ToString());
		}

		[Fact]
		public static void LogMessage_InDarkGray()
		{
			var writer = new StringWriter();
			var sut = new ConsoleRunnerLogger(true, true, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.LogMessage("This is a message");

			Assert.Equal("\u001b[90mThis is a message" + Environment.NewLine + "\u001b[0m", writer.ToString());
		}

		[Fact]
		public static void LogRaw_NoColor()
		{
			var writer = new StringWriter();
			var sut = new ConsoleRunnerLogger(true, true, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.LogRaw("This is a raw message");

			Assert.Equal("This is a raw message" + Environment.NewLine, writer.ToString());
		}

		[Fact]
		public static void LogWarning_InYellow()
		{
			var writer = new StringWriter();
			var sut = new ConsoleRunnerLogger(true, true, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.LogWarning("This is a warning message");

			Assert.Equal("\u001b[93mThis is a warning message" + Environment.NewLine + "\u001b[0m", writer.ToString());
		}
	}

	public static class WaitForAcknowledgment
	{
		[Fact]
		public static void Disabled_DoesNotConsumesStdIn()
		{
			var reader = new StringReader(Environment.NewLine + "abc");
			var sut = new ConsoleRunnerLogger(false, false, new ConsoleHelper(reader, TextWriter.Null), waitForAcknowledgment: false);

			sut.WaitForAcknowledgment();

			Assert.Equal(Environment.NewLine + "abc", reader.ReadToEnd());
		}

		[Fact]
		public static void Enabled_ConsumesOneLineFromStdIn()
		{
			var reader = new StringReader(Environment.NewLine + "abc");
			var sut = new ConsoleRunnerLogger(false, false, new ConsoleHelper(reader, TextWriter.Null), waitForAcknowledgment: true);

			sut.WaitForAcknowledgment();

			Assert.Equal("abc", reader.ReadToEnd());
		}
	}

	public static class WriteLine
	{
		[Fact]
		public static void ColorsEnabled_PlainText()
		{
			var writer = new StringWriter();
			var message = "foo bar";
			var sut = new ConsoleRunnerLogger(true, false, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.WriteLine(message);

			Assert.Equal(message + Environment.NewLine, writer.ToString());
		}

		[Fact]
		public static void ColorsEnabled_AnsiText()
		{
			var writer = new StringWriter();
			var message = "\x1b[3m\x1b[36mhello world\u001b[0m || \x1b[94;103mbright blue on bright yellow\x1b[m";
			var sut = new ConsoleRunnerLogger(true, false, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.WriteLine(message);

			Assert.Equal(message + Environment.NewLine, writer.ToString());
		}

		[Fact]
		public static void ColorsDisabled_PlainText()
		{
			var writer = new StringWriter();
			var message = "foo bar";
			var sut = new ConsoleRunnerLogger(false, false, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.WriteLine(message);

			Assert.Equal(message + Environment.NewLine, writer.ToString());
		}

		[Fact]
		public static void ColorsDisabled_AnsiText()
		{
			var writer = new StringWriter();
			var sut = new ConsoleRunnerLogger(false, false, new ConsoleHelper(TextReader.Null, writer), waitForAcknowledgment: false);

			sut.WriteLine("\x1b[3m\x1b[36mhello world\u001b[0m || \x1b[94;103mbright blue on bright yellow\x1b[m");

			Assert.Equal("hello world || bright blue on bright yellow" + Environment.NewLine, writer.ToString());
		}
	}
}
