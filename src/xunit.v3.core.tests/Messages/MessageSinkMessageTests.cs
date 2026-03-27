using Xunit;
using Xunit.Sdk;
using Xunit.v3;

<<<<<<< HEAD
public partial class MessageSinkMessageTests
=======
public static partial class MessageSinkMessageTests
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
{
	[Fact]
	public static void WithoutJsonTypeID_Throws()
	{
		var msg = new DerivedMessageSinkMessage();

		var ex = Record.Exception(() => msg.ToJson());

		Assert.IsType<InvalidOperationException>(ex);
		Assert.Equal($"Message sink message type '{typeof(DerivedMessageSinkMessage).SafeName()}' is missing its [JsonTypeID] decoration", ex.Message);
	}

	class DerivedMessageSinkMessage : MessageSinkMessage
	{
		protected override void Serialize(JsonObjectSerializer serializer) { }
		protected override void ValidateObjectState(HashSet<string> invalidProperties) { }
	}

	[Fact]
	public static void SerializationExcludesNullValuesAndEmptyTraits()
	{
		var startTime = new DateTimeOffset(2020, 09, 26, 13, 55, 27, 212, TimeSpan.FromHours(-7));
		var msg = new TestAssemblyStarting
		{
			AssemblyUniqueID = "asm-id",
			AssemblyName = "asm-name",
			AssemblyPath = "asm-path",
			ConfigFilePath = null,
			Seed = null,
			StartTime = startTime,
			TargetFramework = null,
			TestEnvironment = "test-env",
			TestFrameworkDisplayName = "test-framework",
			Traits = TestData.EmptyTraits,
		};

		var json = msg.ToJson();

		Assert.NotNull(json);
		var expected =
@"{
    ""$type"":                    ""test-assembly-starting"",
    ""AssemblyUniqueID"":         ""asm-id"",
    ""AssemblyName"":             ""asm-name"",
    ""AssemblyPath"":             ""asm-path"",
    ""StartTime"":                ""2020-09-26T13:55:27.2120000-07:00"",
    ""TestEnvironment"":          ""test-env"",
    ""TestFrameworkDisplayName"": ""test-framework""
}".Replace("\n", "").Replace(" ", "");
		Assert.Equal(expected, json);
	}

	[Fact]
	public static void SerializesEnumsAsStringsAndExcludesEmptyOutput()
	{
		var finishTime = new DateTimeOffset(2020, 09, 26, 13, 55, 27, 212, TimeSpan.FromHours(-7));
		var msg = new TestFailed
		{
			AssemblyUniqueID = "asm-id",
			Cause = FailureCause.Assertion,
			ExceptionParentIndices = [-1],
			ExceptionTypes = ["exception-type"],
			ExecutionTime = 123.45m,
			FinishTime = finishTime,
			Messages = ["exception-message"],
			Output = "",
			StackTraces = [null],
			TestCaseUniqueID = "test-case-id",
			TestClassUniqueID = null,
			TestCollectionUniqueID = "test-collection-id",
			TestMethodUniqueID = null,
			TestUniqueID = "test-id",
			Warnings = null,
		};

		var json = msg.ToJson();

		Assert.NotNull(json);
		var expected =
@"{
    ""$type"":                  ""test-failed"",
    ""AssemblyUniqueID"":       ""asm-id"",
    ""TestCollectionUniqueID"": ""test-collection-id"",
    ""TestCaseUniqueID"":       ""test-case-id"",
    ""TestUniqueID"":           ""test-id"",
    ""ExecutionTime"":          123.45,
    ""FinishTime"":             ""2020-09-26T13:55:27.2120000-07:00"",
    ""Cause"":                  ""Assertion"",
    ""ExceptionParentIndices"": [-1],
    ""ExceptionTypes"":         [""exception-type""],
    ""Messages"":               [""exception-message""],
    ""StackTraces"":            [null]
}".Replace("\n", "").Replace(" ", "");
		Assert.Equal(expected, json);
	}
}
