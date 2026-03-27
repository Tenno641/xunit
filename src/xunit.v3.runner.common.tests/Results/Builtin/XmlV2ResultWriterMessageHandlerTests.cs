using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Xunit;
using Xunit.Runner.Common;
using Xunit.Sdk;

[Collection(typeof(CleanEnvironmentAttribute))]
[CleanEnvironment("COMPUTERNAME", "HOSTNAME", "NAME", "HOST", "USERNAME", "LOGNAME", "USER")]
<<<<<<< HEAD
public class XmlV2ResultWriterMessageHandlerTests
=======
public static class XmlV2ResultWriterMessageHandlerTests
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
{
	[Theory]
	// Windows
	[InlineData("COMPUTERNAME", "USERNAME")]
	// Linux
	[InlineData("HOSTNAME", "LOGNAME")]
	[InlineData("HOSTNAME", "USER")]
	[InlineData("NAME", "LOGNAME")]
	[InlineData("NAME", "USER")]
	// macOS
	[InlineData("HOST", "LOGNAME")]
	[InlineData("HOST", "USER")]
	public static async ValueTask AssembliesElement(
		string computerEnvName,
		string userEnvName)
	{
		Environment.SetEnvironmentVariable(computerEnvName, "expected-computer");
		Environment.SetEnvironmentVariable(userEnvName, "expected-user");

		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		var assembliesElement = await handler.AssembliesElement();
		Assert.Equal("expected-computer", assembliesElement.Attribute("computer")?.Value);
		Guid.Parse(assembliesElement.Attribute("id")!.Value);  // Just ensure it's a GUID, it's randomized every time
		Assert.Equal("3", assembliesElement.Attribute("schema-version")?.Value);
		Assert.Equal("expected-user", assembliesElement.Attribute("user")?.Value);
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask AssembliesElementDoesNotIncludeOptionalValues()
=======
	public static async ValueTask AssembliesElementDoesNotIncludeOptionalValues()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		var assembliesElement = await handler.AssembliesElement();
		Assert.Null(assembliesElement.Attribute("computer"));
		Assert.Null(assembliesElement.Attribute("user"));
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask AssemblyStarting()
=======
	public static async ValueTask AssemblyStarting()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var startTime = new DateTimeOffset(2013, 7, 6, 16, 24, 32, TimeSpan.Zero);
		var assemblyStarting = TestData.TestAssemblyStarting(
			assemblyPath: "/path/to/assembly.dll",
			configFilePath: "config",
			startTime: startTime,
			targetFramework: "MentalFloss,Version=v21.12",
			testEnvironment: "256-bit MentalFloss",
			testFrameworkDisplayName: "xUnit.net v14.42"
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);

		var assemblyElement = await handler.AssemblyElement();
		Assert.Equal("config", assemblyElement.Attribute("config-file")?.Value);
		Assert.Equal("256-bit MentalFloss", assemblyElement.Attribute("environment")?.Value);
		Guid.Parse(assemblyElement.Attribute("id")!.Value);  // Just ensure it's a GUID, it's randomized every time
		Assert.Equal("/path/to/assembly.dll", assemblyElement.Attribute("name")?.Value);
		Assert.Equal("2013-07-06", assemblyElement.Attribute("run-date")?.Value);
		Assert.Equal("16:24:32", assemblyElement.Attribute("run-time")?.Value);
		Assert.Equal(startTime.ToRtf(), assemblyElement.Attribute("start-rtf")?.Value);
		Assert.Equal("MentalFloss,Version=v21.12", assemblyElement.Attribute("target-framework")?.Value);
		Assert.Equal("xUnit.net v14.42", assemblyElement.Attribute("test-framework")?.Value);
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask AssemblyStartingDoesNotIncludeOptionalValues()
=======
	public static async ValueTask AssemblyStartingDoesNotIncludeOptionalValues()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyStarting = TestData.TestAssemblyStarting(assemblyPath: "/path/to/assembly.dll", configFilePath: null, targetFramework: null);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);

		var assemblyElement = await handler.AssemblyElement();
		Assert.Null(assemblyElement.Attribute("config-file"));
		Assert.Null(assemblyElement.Attribute("target-framework"));
	}

	[CulturedFact(["en-US", "fr-FR"])]
	public static async ValueTask AssemblyFinished()
	{
		var finishTime = new DateTimeOffset(2013, 7, 6, 17, 32, 48, TimeSpan.Zero);
		var assemblyStarting = TestData.TestAssemblyStarting();
		var assemblyFinished = TestData.TestAssemblyFinished(
			executionTime: 123.4567M,
			finishTime: finishTime,
			testsFailed: 42,
			testsNotRun: 3,
			testsSkipped: 6,
			testsTotal: 2112
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();
		var errorMessage = TestData.ErrorMessage(
			exceptionParentIndices: [-1],
			exceptionTypes: ["ExceptionType"],
			messages: ["Message"],
			stackTraces: ["Stack"]
		);

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(errorMessage);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		Assert.Equal("1", assemblyElement.Attribute("errors")?.Value);
		Assert.Equal("42", assemblyElement.Attribute("failed")?.Value);
		Assert.Equal(finishTime.ToRtf(), assemblyElement.Attribute("finish-rtf")?.Value);
		Assert.Equal("3", assemblyElement.Attribute("not-run")?.Value);
		Assert.Equal("2061", assemblyElement.Attribute("passed")?.Value);
		Assert.Equal("6", assemblyElement.Attribute("skipped")?.Value);
		Assert.Equal(123.457M.ToString(CultureInfo.InvariantCulture), assemblyElement.Attribute("time")?.Value);
		Assert.Equal(123.4567M.ToTimespanRtf(), assemblyElement.Attribute("time-rtf")?.Value);
		Assert.Equal("2112", assemblyElement.Attribute("total")?.Value);
	}

	[CulturedFact(["en-US", "fr-FR"])]
	public static async ValueTask TestCollections()
	{
		var assemblyStarting = TestData.TestAssemblyStarting();
		var assemblyFinished = TestData.TestAssemblyFinished();
		var testCollectionStarted = TestData.TestCollectionStarting(testCollectionDisplayName: "Collection Name", testCollectionUniqueID: "abc123");
		var testCollectionFinished = TestData.TestCollectionFinished(testsTotal: 2112, testsFailed: 42, testsSkipped: 6, testsNotRun: 3, executionTime: 123.4567m, testCollectionUniqueID: "abc123");
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(testCollectionStarted);
		handler.OnMessage(testCollectionFinished);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var collectionElement = Assert.Single(assemblyElement.Elements("collection"));
		Assert.Equal("Collection Name", collectionElement.Attribute("name")?.Value);
		Assert.Equal("2112", collectionElement.Attribute("total")?.Value);
		Assert.Equal("2061", collectionElement.Attribute("passed")?.Value);
		Assert.Equal("42", collectionElement.Attribute("failed")?.Value);
		Assert.Equal("6", collectionElement.Attribute("skipped")?.Value);
		Assert.Equal("3", collectionElement.Attribute("not-run")?.Value);
		Assert.Equal(123.457M.ToString(CultureInfo.InvariantCulture), collectionElement.Attribute("time")?.Value);
	}

	[CulturedFact(["en-US", "fr-FR"])]
<<<<<<< HEAD
	public async ValueTask TestPassed()
=======
	public static async ValueTask TestPassed()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting(testClassName: typeof(ClassUnderTest).FullName!);
		var methodStarting = TestData.TestMethodStarting(methodName: nameof(ClassUnderTest.TestMethod));
		var caseStarting = TestData.TestCaseStarting(traits: TestData.EmptyTraits);
		var testStarting = TestData.TestStarting(testDisplayName: "Test Display Name");
		var testPassed = TestData.TestPassed(executionTime: 123.4567809m, output: "test output");
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testPassed);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var testElement = Assert.Single(assemblyElement.Elements("collection").Single().Elements("test"));
		Assert.Equal("Test Display Name", testElement.Attribute("name")?.Value);
		Assert.Equal(typeof(ClassUnderTest).FullName, testElement.Attribute("type")?.Value);
		Assert.Equal("TestMethod", testElement.Attribute("method")?.Value);
		Assert.Equal("Pass", testElement.Attribute("result")?.Value);
		Assert.Equal(123.4567809M.ToString(CultureInfo.InvariantCulture), testElement.Attribute("time")?.Value);
		Assert.Equal("test output", testElement.Element("output")?.Value);
		Assert.Null(testElement.Attribute("source-file"));
		Assert.Null(testElement.Attribute("source-line"));
		Assert.Empty(testElement.Elements("traits"));
		Assert.Empty(testElement.Elements("failure"));
		Assert.Empty(testElement.Elements("reason"));
	}

	[CulturedFact(["en-US", "fr-FR"])]
<<<<<<< HEAD
	public async ValueTask TestFailed()
=======
	public static async ValueTask TestFailed()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting(testClassName: typeof(ClassUnderTest).FullName!);
		var methodStarting = TestData.TestMethodStarting(methodName: nameof(ClassUnderTest.TestMethod));
		var caseStarting = TestData.TestCaseStarting();
		var testStarting = TestData.TestStarting(testDisplayName: "Test Display Name");
		var testFailed = TestData.TestFailed(
			exceptionParentIndices: [-1],
			exceptionTypes: ["Exception Type"],
			executionTime: 123.4567809m,
			messages: ["Exception Message"],
			output: "test output",
			stackTraces: ["Exception Stack Trace"]
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testFailed);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var testElement = Assert.Single(assemblyElement.Elements("collection").Single().Elements("test"));
		Assert.Equal("Test Display Name", testElement.Attribute("name")?.Value);
		Assert.Equal(typeof(ClassUnderTest).FullName, testElement.Attribute("type")?.Value);
		Assert.Equal("TestMethod", testElement.Attribute("method")?.Value);
		Assert.Equal("Fail", testElement.Attribute("result")?.Value);
		Assert.Equal(123.4567809M.ToString(CultureInfo.InvariantCulture), testElement.Attribute("time")?.Value);
		Assert.Equal("test output", testElement.Element("output")?.Value);
		var failureElement = Assert.Single(testElement.Elements("failure"));
		Assert.Equal("Exception Type", failureElement.Attribute("exception-type")?.Value);
		Assert.Equal("Exception Type : Exception Message", failureElement.Elements("message").Single().Value);
		Assert.Equal("Exception Stack Trace", failureElement.Elements("stack-trace").Single().Value);
		Assert.Empty(testElement.Elements("reason"));
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestFailed_NullStackTrace()
=======
	public static async ValueTask TestFailed_NullStackTrace()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting(testClassName: typeof(ClassUnderTest).FullName!);
		var methodStarting = TestData.TestMethodStarting(methodName: nameof(ClassUnderTest.TestMethod));
		var caseStarting = TestData.TestCaseStarting();
		var testStarting = TestData.TestStarting(testDisplayName: "Test Display Name");
		var testFailed = TestData.TestFailed(
			exceptionParentIndices: [-1],
			exceptionTypes: ["Exception Type"],
			executionTime: 123.4567809m,
			messages: ["Exception Message"],
			output: "test output",
			stackTraces: [default]
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testFailed);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var testElement = Assert.Single(assemblyElement.Elements("collection").Single().Elements("test"));
		var failureElement = Assert.Single(testElement.Elements("failure"));
		Assert.Empty(failureElement.Elements("stack-trace").Single().Value);
	}

	[CulturedFact(["en-US", "fr-FR"])]
<<<<<<< HEAD
	public async ValueTask TestSkipped()
=======
	public static async ValueTask TestSkipped()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting(testClassName: typeof(ClassUnderTest).FullName!);
		var methodStarting = TestData.TestMethodStarting(methodName: nameof(ClassUnderTest.TestMethod));
		var caseStarting = TestData.TestCaseStarting();
		var testStarting = TestData.TestStarting(testDisplayName: "Test Display Name");
		var testSkipped = TestData.TestSkipped(reason: "Skip Reason");
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testSkipped);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var testElement = Assert.Single(assemblyElement.Elements("collection").Single().Elements("test"));
		Assert.Equal("Test Display Name", testElement.Attribute("name")?.Value);
		Assert.Equal(typeof(ClassUnderTest).FullName, testElement.Attribute("type")?.Value);
		Assert.Equal("TestMethod", testElement.Attribute("method")?.Value);
		Assert.Equal("Skip", testElement.Attribute("result")?.Value);
		Assert.Equal(0m.ToString(CultureInfo.InvariantCulture), testElement.Attribute("time")?.Value);
		var reasonElement = Assert.Single(testElement.Elements("reason"));
		Assert.Equal("Skip Reason", reasonElement.Value);
		Assert.Empty(testElement.Elements("failure"));
	}

	[CulturedFact(["en-US", "fr-FR"])]
<<<<<<< HEAD
	public async ValueTask TestNotRun()
=======
	public static async ValueTask TestNotRun()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting(testClassName: typeof(ClassUnderTest).FullName!);
		var methodStarting = TestData.TestMethodStarting(methodName: nameof(ClassUnderTest.TestMethod));
		var caseStarting = TestData.TestCaseStarting();
		var testStarting = TestData.TestStarting(testDisplayName: "Test Display Name");
		var testNotRun = TestData.TestNotRun();
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testNotRun);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var testElement = Assert.Single(assemblyElement.Elements("collection").Single().Elements("test"));
		Assert.Equal("Test Display Name", testElement.Attribute("name")?.Value);
		Assert.Equal(typeof(ClassUnderTest).FullName, testElement.Attribute("type")?.Value);
		Assert.Equal("TestMethod", testElement.Attribute("method")?.Value);
		Assert.Equal("NotRun", testElement.Attribute("result")?.Value);
		Assert.Equal(0m.ToString(CultureInfo.InvariantCulture), testElement.Attribute("time")?.Value);
		Assert.Empty(testElement.Elements("failure"));
	}

	[CulturedFact(["en-US", "fr-FR"])]
<<<<<<< HEAD
	public async ValueTask TestResult_EmptyOutput()
=======
	public static async ValueTask TestResult_EmptyOutput()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting(testClassName: typeof(ClassUnderTest).FullName!);
		var methodStarting = TestData.TestMethodStarting(methodName: nameof(ClassUnderTest.TestMethod));
		var caseStarting = TestData.TestCaseStarting(traits: TestData.EmptyTraits);
		var testStarting = TestData.TestStarting(testDisplayName: "Test Display Name");
		var testPassed = TestData.TestPassed(executionTime: 123.4567809m, output: "");
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testPassed);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var testElement = Assert.Single(assemblyElement.Elements("collection").Single().Elements("test"));
		Assert.Equal("Test Display Name", testElement.Attribute("name")?.Value);
		Assert.Equal(typeof(ClassUnderTest).FullName, testElement.Attribute("type")?.Value);
		Assert.Equal("TestMethod", testElement.Attribute("method")?.Value);
		Assert.Equal("Pass", testElement.Attribute("result")?.Value);
		Assert.Equal(123.4567809M.ToString(CultureInfo.InvariantCulture), testElement.Attribute("time")?.Value);
		Assert.Null(testElement.Attribute("output"));
		Assert.Null(testElement.Attribute("source-file"));
		Assert.Null(testElement.Attribute("source-line"));
		Assert.Empty(testElement.Elements("traits"));
		Assert.Empty(testElement.Elements("failure"));
		Assert.Empty(testElement.Elements("reason"));
	}

	[CulturedFact(["en-US", "fr-FR"])]
<<<<<<< HEAD
	public async ValueTask TestResult_OutputIsEscaped()
=======
	public static async ValueTask TestResult_OutputIsEscaped()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting(testClassName: typeof(ClassUnderTest).FullName!);
		var methodStarting = TestData.TestMethodStarting(methodName: nameof(ClassUnderTest.TestMethod));
		var caseStarting = TestData.TestCaseStarting(traits: TestData.EmptyTraits);
		var testStarting = TestData.TestStarting(testDisplayName: "Test Display Name");
		var testPassed = TestData.TestPassed(executionTime: 123.4567809m, output: "\u001B[31mtest output");
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testPassed);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var testElement = Assert.Single(assemblyElement.Elements("collection").Single().Elements("test"));
		Assert.Equal("Test Display Name", testElement.Attribute("name")?.Value);
		Assert.Equal(typeof(ClassUnderTest).FullName, testElement.Attribute("type")?.Value);
		Assert.Equal("TestMethod", testElement.Attribute("method")?.Value);
		Assert.Equal("Pass", testElement.Attribute("result")?.Value);
		Assert.Equal(123.4567809M.ToString(CultureInfo.InvariantCulture), testElement.Attribute("time")?.Value);
		Assert.Equal("test output", testElement.Element("output")?.Value);
		Assert.Null(testElement.Attribute("source-file"));
		Assert.Null(testElement.Attribute("source-line"));
		Assert.Empty(testElement.Elements("traits"));
		Assert.Empty(testElement.Elements("failure"));
		Assert.Empty(testElement.Elements("reason"));
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestResult_WithSourceInformation()
=======
	public static async ValueTask TestResult_WithSourceInformation()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting();
		var methodStarting = TestData.TestMethodStarting();
		var caseStarting = TestData.TestCaseStarting(sourceFilePath: "source file", sourceLineNumber: 42);
		var testStarting = TestData.TestStarting();
		var testPassed = TestData.TestPassed();
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testPassed);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var testElement = Assert.Single(assemblyElement.Elements("collection").Single().Elements("test"));
		Assert.Equal("source file", testElement.Attribute("source-file")?.Value);
		Assert.Equal("42", testElement.Attribute("source-line")?.Value);
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestResult_WithTraits()
=======
	public static async ValueTask TestResult_WithTraits()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var traits = new Dictionary<string, IReadOnlyCollection<string>>
		{
			{ "name1", new List<string> { "value1" }},
			{ "name2", new List<string> { "value2" }}
		};
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting();
		var methodStarting = TestData.TestMethodStarting();
		var caseStarting = TestData.TestCaseStarting(traits: traits);
		var testStarting = TestData.TestStarting();
		var testPassed = TestData.TestPassed();
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testPassed);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var traitsElements = assemblyElement.Elements("collection").Single().Elements("test").Single().Elements("traits").Single().Elements("trait");
		var name1Element = Assert.Single(traitsElements, e => e.Attribute("name")?.Value == "name1");
		Assert.Equal("value1", name1Element.Attribute("value")?.Value);
		var name2Element = Assert.Single(traitsElements, e => e.Attribute("name")?.Value == "name2");
		Assert.Equal("value2", name2Element.Attribute("value")?.Value);
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestResult_WithAttachments()
=======
	public static async ValueTask TestResult_WithAttachments()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var attachments = new Dictionary<string, TestAttachment>
		{
			{ "hello", TestAttachment.Create("world") },
			{ "bytes", TestAttachment.Create([1, 2, 3], "application/octet-stream") },
		};
		var assemblyFinished = TestData.TestAssemblyFinished();
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting();
		var classStarting = TestData.TestClassStarting();
		var methodStarting = TestData.TestMethodStarting();
		var caseStarting = TestData.TestCaseStarting();
		var testStarting = TestData.TestStarting();
		var testPassed = TestData.TestPassed();
		var testFinished = TestData.TestFinished(attachments: attachments);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testPassed);
		handler.OnMessage(testFinished);
		handler.OnMessage(assemblyFinished);

		var assemblyElement = await handler.AssemblyElement();
		var attachmentsElement = assemblyElement.Elements("collection").Single().Elements("test").Single().Element("attachments");
		Assert.NotNull(attachmentsElement);
		Assert.Collection(
			attachmentsElement.Elements("attachment").OrderBy(element => element.Attribute("name")?.Value),
			element =>
			{
				Assert.Equal("bytes", element.Attribute("name")?.Value);
				Assert.Equal("application/octet-stream", element.Attribute("media-type")?.Value);
				Assert.Equal("AQID", element.Value);
			},
			element =>
			{
				Assert.Equal("hello", element.Attribute("name")?.Value);
				Assert.Equal("world", element.Value);
			}
		);
	}

	public static IEnumerable<TheoryDataRow<string, string>> IllegalXmlTestData =>
	[
		(
			new string(Enumerable.Range(0, 32).Select(x => (char)x).ToArray()),
			@"\0\x01\x02\x03\x04\x05\x06\a\b\t\n\v\f\r\x0e\x0f\x10\x11\x12\x13\x14\x15\x16\x17\x18\x19\x1a\x1b\x1c\x1d\x1e\x1f"
		),
		// Invalid surrogate characters should be added as \x----, where ---- is the hex value of the char
		(
			"\xd800 Hello.World \xdc00",
			@"\xd800 Hello.World \xdc00"
		),
		// ...but valid ones should be outputted like normal
		(
			"\xd800\xdfff This.Is.Valid \xda00\xdd00",
			"\xd800\xdfff This.Is.Valid \xda00\xdd00" // note: no @
		),
	];

	[Theory]
	[MemberData(nameof(IllegalXmlTestData))]
<<<<<<< HEAD
	public async ValueTask TestResult_WithIllegalStringValue(
=======
	public static async ValueTask TestResult_WithIllegalStringValue(
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		string inputName,
		string outputName)
	{
		var classStarting = TestData.TestClassStarting(testClassName: typeof(ClassUnderTest).FullName!);
		var methodStarting = TestData.TestMethodStarting(methodName: nameof(ClassUnderTest.TestMethod));
		var testStarting = TestData.TestStarting(testDisplayName: inputName);
		var testSkipped = TestData.TestSkipped(reason: "Bad\0\r\nString");
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(TestData.TestAssemblyStarting());
		handler.OnMessage(TestData.TestCollectionStarting());
		handler.OnMessage(classStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(TestData.TestCaseStarting());
		handler.OnMessage(testStarting);
		handler.OnMessage(testSkipped);
		handler.OnMessage(TestData.TestFinished());
		handler.OnMessage(TestData.TestCaseFinished());
		handler.OnMessage(TestData.TestMethodFinished());
		handler.OnMessage(TestData.TestClassFinished());
		handler.OnMessage(TestData.TestCollectionFinished());
		handler.OnMessage(TestData.TestAssemblyFinished());

		var assemblyElement = await handler.AssemblyElement();
		var testElement = assemblyElement.XPathSelectElement("collection/test");
		Assert.NotNull(testElement);
		Assert.Equal(outputName, testElement.Attribute("name")?.Value);
		var reasonElement = testElement.XPathSelectElement("reason");
		Assert.NotNull(reasonElement);
		Assert.Equal("Bad\\0\nString", reasonElement.Value);
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask ErrorMessage()
=======
	public static async ValueTask ErrorMessage()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyStarting = TestData.TestAssemblyStarting();
		var errorMessage = TestData.ErrorMessage(
			exceptionParentIndices: exceptionParentIndices,
			exceptionTypes: exceptionTypes,
			messages: messages,
			stackTraces: stackTraces
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(errorMessage);

		var assemblyElement = await handler.AssemblyElement();
		AssertFailureElement(assemblyElement, "fatal", null);
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestAssemblyCleanupFailure()
=======
	public static async ValueTask TestAssemblyCleanupFailure()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyStarting = TestData.TestAssemblyStarting(
			assemblyUniqueID: assemblyID,
			assemblyName: "assembly-name",
			assemblyPath: "assembly-file-path",
			configFilePath: "config-file-path",
			startTime: DateTimeOffset.UtcNow,
			targetFramework: "target-framework",
			testEnvironment: "test-environment",
			testFrameworkDisplayName: "test-framework"
		);
		var assemblyCleanupFailure = TestData.TestAssemblyCleanupFailure(
			assemblyUniqueID: assemblyID,
			exceptionParentIndices: exceptionParentIndices,
			exceptionTypes: exceptionTypes,
			messages: messages,
			stackTraces: stackTraces
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(assemblyCleanupFailure);

		var assemblyElement = await handler.AssemblyElement();
		AssertFailureElement(assemblyElement, "assembly-cleanup", "assembly-file-path");
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestCollectionCleanupFailure()
=======
	public static async ValueTask TestCollectionCleanupFailure()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyStarting = TestData.TestAssemblyStarting();
		var collectionStarting = TestData.TestCollectionStarting(
			assemblyUniqueID: assemblyID,
			testCollectionDisplayName: "FooBar",
			testCollectionUniqueID: collectionID
		);
		var collectionCleanupFailure = TestData.TestCollectionCleanupFailure(
			assemblyUniqueID: assemblyID,
			exceptionParentIndices: exceptionParentIndices,
			exceptionTypes: exceptionTypes,
			messages: messages,
			stackTraces: stackTraces,
			testCollectionUniqueID: collectionID
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(collectionStarting);
		handler.OnMessage(collectionCleanupFailure);

		var assemblyElement = await handler.AssemblyElement();
		AssertFailureElement(assemblyElement, "test-collection-cleanup", "FooBar");
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestClassCleanupFailure()
=======
	public static async ValueTask TestClassCleanupFailure()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyStarting = TestData.TestAssemblyStarting();
		var classStarting = TestData.TestClassStarting(
			assemblyUniqueID: assemblyID,
			testClassName: "MyType",
			testClassUniqueID: classID,
			testCollectionUniqueID: collectionID
		);
		var classCleanupFailure = TestData.TestClassCleanupFailure(
			assemblyUniqueID: assemblyID,
			exceptionParentIndices: exceptionParentIndices,
			exceptionTypes: exceptionTypes,
			messages: messages,
			testClassUniqueID: classID,
			testCollectionUniqueID: collectionID,
			stackTraces: stackTraces
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(classStarting);
		handler.OnMessage(classCleanupFailure);

		var assemblyElement = await handler.AssemblyElement();
		AssertFailureElement(assemblyElement, "test-class-cleanup", "MyType");
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestMethodCleanupFailure()
=======
	public static async ValueTask TestMethodCleanupFailure()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyStarting = TestData.TestAssemblyStarting();
		var methodStarting = TestData.TestMethodStarting(
			assemblyUniqueID: assemblyID,
			testClassUniqueID: classID,
			testCollectionUniqueID: collectionID,
			methodName: "MyMethod",
			testMethodUniqueID: methodID
		);
		var methodCleanupFailure = TestData.TestMethodCleanupFailure(
			assemblyUniqueID: assemblyID,
			exceptionParentIndices: exceptionParentIndices,
			exceptionTypes: exceptionTypes,
			messages: messages,
			stackTraces: stackTraces,
			testCollectionUniqueID: collectionID,
			testClassUniqueID: classID,
			testMethodUniqueID: methodID
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(methodStarting);
		handler.OnMessage(methodCleanupFailure);

		var assemblyElement = await handler.AssemblyElement();
		AssertFailureElement(assemblyElement, "test-method-cleanup", "MyMethod");
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestCaseCleanupFailure()
=======
	public static async ValueTask TestCaseCleanupFailure()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyStarting = TestData.TestAssemblyStarting();
		var caseStarting = TestData.TestCaseStarting(
			assemblyUniqueID: assemblyID,
			testCaseUniqueID: testCaseID,
			testCaseDisplayName: "MyTestCase",
			testClassUniqueID: classID,
			testCollectionUniqueID: collectionID,
			testMethodUniqueID: methodID
		);
		var caseCleanupFailure = TestData.TestCaseCleanupFailure(
			assemblyUniqueID: assemblyID,
			exceptionParentIndices: exceptionParentIndices,
			exceptionTypes: exceptionTypes,
			messages: messages,
			stackTraces: stackTraces,
			testCaseUniqueID: testCaseID,
			testCollectionUniqueID: collectionID,
			testClassUniqueID: classID,
			testMethodUniqueID: methodID
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(caseStarting);
		handler.OnMessage(caseCleanupFailure);

		var assemblyElement = await handler.AssemblyElement();
		AssertFailureElement(assemblyElement, "test-case-cleanup", "MyTestCase");
	}

	[Fact]
<<<<<<< HEAD
	public async ValueTask TestCleanupFailure()
=======
	public static async ValueTask TestCleanupFailure()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyStarting = TestData.TestAssemblyStarting();
		var testStarting = TestData.TestStarting(
			assemblyUniqueID: assemblyID,
			testCaseUniqueID: testCaseID,
			testClassUniqueID: classID,
			testCollectionUniqueID: collectionID,
			testDisplayName: "MyTest",
			testMethodUniqueID: methodID,
			testUniqueID: testID
		);
		var testCleanupFailure = TestData.TestCleanupFailure(
			assemblyUniqueID: assemblyID,
			exceptionParentIndices: exceptionParentIndices,
			exceptionTypes: exceptionTypes,
			messages: messages,
			stackTraces: stackTraces,
			testCaseUniqueID: testCaseID,
			testCollectionUniqueID: collectionID,
			testClassUniqueID: classID,
			testMethodUniqueID: methodID,
			testUniqueID: testID
		);
		await using var handler = TestableXmlV2ResultWriterMessageHandler.Create();

		handler.OnMessage(assemblyStarting);
		handler.OnMessage(testStarting);
		handler.OnMessage(testCleanupFailure);

		var assemblyElement = await handler.AssemblyElement();
		AssertFailureElement(assemblyElement, "test-cleanup", "MyTest");
	}

	class ClassUnderTest
	{
		[Fact]
		public async ValueTask TestMethod() { }
	}

<<<<<<< HEAD
	readonly string assemblyID = "assembly-id";
	readonly string classID = "test-class-id";
	readonly string collectionID = "test-collection-id";
	readonly int[] exceptionParentIndices = [-1];
	readonly string[] exceptionTypes = ["ExceptionType"];
	readonly string[] messages = ["This is\t\r\nmy message"];
	readonly string methodID = "test-method-id";
	readonly string[] stackTraces = ["Line 1\r\nLine 2\r\nLine 3"];
	readonly string testCaseID = "test-case-id";
	readonly string testID = "test-id";
=======
	static readonly string assemblyID = "assembly-id";
	static readonly string classID = "test-class-id";
	static readonly string collectionID = "test-collection-id";
	static readonly int[] exceptionParentIndices = [-1];
	static readonly string[] exceptionTypes = ["ExceptionType"];
	static readonly string[] messages = ["This is\t\r\nmy message"];
	static readonly string methodID = "test-method-id";
	static readonly string[] stackTraces = ["Line 1\r\nLine 2\r\nLine 3"];
	static readonly string testCaseID = "test-case-id";
	static readonly string testID = "test-id";
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940

	static void AssertFailureElement(
		XElement assemblyElement,
		string messageType,
		string? name)
	{
		var errorElement = Assert.Single(assemblyElement.Element("errors")!.Elements());
		Assert.Equal(messageType, errorElement.Attribute("type")?.Value);

		if (name is null)
			Assert.Null(errorElement.Attribute("name"));
		else
			Assert.Equal(name, errorElement.Attribute("name")?.Value);

		var failureElement = Assert.Single(errorElement.Elements("failure"));
		Assert.Equal("ExceptionType", failureElement.Attribute("exception-type")?.Value);
		Assert.Equal("ExceptionType : This is\\t\nmy message", failureElement.Elements("message").Single().Value);
		Assert.Equal("Line 1\nLine 2\nLine 3", failureElement.Elements("stack-trace").Single().Value);
	}

	class TestableXmlV2ResultWriterMessageHandler(
		StringWriter stringWriter,
		XmlWriter xmlWriter) :
			XmlV2ResultWriterMessageHandler(xmlWriter)
	{
		public async ValueTask<XElement> AssembliesElement()
		{
			await DisposeAsync();

			var document = XDocument.Parse(stringWriter.ToString());
			var assemblyElement = document.XPathSelectElement("/assemblies");
			Assert.NotNull(assemblyElement);

			return assemblyElement;
		}

		public async ValueTask<XElement> AssemblyElement()
		{
			await DisposeAsync();

			var document = XDocument.Parse(stringWriter.ToString());
			var assemblyElement = document.XPathSelectElement("/assemblies/assembly");
			Assert.NotNull(assemblyElement);

			return assemblyElement;
		}

		public static TestableXmlV2ResultWriterMessageHandler Create()
		{
			var stringWriter = new StringWriter();
			var textWriter = new XmlTextWriter(stringWriter);

			return new TestableXmlV2ResultWriterMessageHandler(stringWriter, textWriter);
		}
	}
}
