#pragma warning disable IDE0290  // Lots of things in here can't use primary constructors

using Xunit;
using Xunit.Sdk;

<<<<<<< HEAD
public partial class FixtureAcceptanceTests
=======
public static partial class FixtureAcceptanceTests
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
{
	public partial class AsyncClassFixture : AcceptanceTestV3
	{
		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureDataShouldHaveBeenSetup()
=======
		public static async ValueTask FixtureDataShouldHaveBeenSetup()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+AsyncClassFixture+FixtureSpy");
#else
			var messages = await RunForResultsAsync(typeof(FixtureSpy));
#endif

			Assert.Single(messages.OfType<TestPassedWithMetadata>());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask ThrowingFixtureInitializeAsyncShouldResultInFailedTest()
=======
		public static async ValueTask ThrowingFixtureInitializeAsyncShouldResultInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+AsyncClassFixture+ClassWithThrowingFixtureInitializeAsync");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithThrowingFixtureInitializeAsync));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Class fixture type 'FixtureAcceptanceTests+ThrowingInitializeAsyncFixture' threw in InitializeAsync", msg.Messages.First());
		}

		[Fact]
		public static async ValueTask TestClassWithThrowingFixtureAsyncDisposeResultsInFailedTest()
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestClassCleanupFailure>("FixtureAcceptanceTests+AsyncClassFixture+ClassWithThrowingFixtureDisposeAsync");
#else
			var messages = await RunAsync<ITestClassCleanupFailure>(typeof(ClassWithThrowingFixtureDisposeAsync));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Class fixture type 'FixtureAcceptanceTests+ThrowingDisposeAsyncFixture' threw in DisposeAsync", msg.Messages.First());
		}
	}

	public partial class AsyncCollectionFixture : AcceptanceTestV3
	{
		[Fact]
<<<<<<< HEAD
		public async ValueTask CollectionFixtureAsyncSetupShouldOnlyRunOnce()
=======
		public static async ValueTask CollectionFixtureAsyncSetupShouldOnlyRunOnce()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var results = await RunForResultsAsync(["FixtureAcceptanceTests+AsyncCollectionFixture+Fixture1", "FixtureAcceptanceTests+AsyncCollectionFixture+Fixture2"]);
#else
			var results = await RunForResultsAsync([typeof(Fixture1), typeof(Fixture2)]);
#endif

			Assert.Equal(2, results.OfType<TestPassedWithMetadata>().Count());
		}

		[Fact]
		public static async ValueTask TestClassWithThrowingCollectionFixtureSetupAsyncResultsInFailedTest()
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+AsyncCollectionFixture+ClassWithThrowingFixtureInitializeAsync");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithThrowingFixtureInitializeAsync));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Collection fixture type 'FixtureAcceptanceTests+ThrowingInitializeAsyncFixture' threw in InitializeAsync", msg.Messages.First());
		}

		[Fact]
		public static async ValueTask TestClassWithThrowingCollectionFixtureDisposeAsyncResultsInFailedTest()
		{
#if XUNIT_AOT
			var messages = await RunAsync("FixtureAcceptanceTests+AsyncCollectionFixture+ClassWithThrowingFixtureDisposeAsync");
#else
			var messages = await RunAsync<ITestCollectionCleanupFailure>(typeof(ClassWithThrowingFixtureDisposeAsync));
#endif

			var msg = Assert.Single(messages.OfType<ITestCollectionCleanupFailure>());
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Collection fixture type 'FixtureAcceptanceTests+ThrowingDisposeAsyncFixture' threw in DisposeAsync", msg.Messages.First());
		}
	}

	public partial class ClassFixture : AcceptanceTestV3
	{
		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithMissingArgumentToConstructorIsAcceptable()
=======
		public static async ValueTask TestClassWithMissingArgumentToConstructorIsAcceptable()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+ClassFixture+ClassWithMissingCtorArg");
#else
			var messages = await RunAsync<ITestPassed>(typeof(ClassWithMissingCtorArg));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithoutCtorWithThrowingFixtureConstructorResultsInFailedTest()
=======
		public static async ValueTask TestClassWithoutCtorWithThrowingFixtureConstructorResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+ClassFixture+ClassWithThrowingFixtureCtor");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithThrowingFixtureCtor));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Class fixture type 'FixtureAcceptanceTests+ThrowingCtorFixture' threw in its constructor", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithCtorWithThrowingFixtureConstructorResultsInFailedTest()
=======
		public static async ValueTask TestClassWithCtorWithThrowingFixtureConstructorResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+ClassFixture+ClassWithCtorAndThrowingFixtureCtor");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithCtorAndThrowingFixtureCtor));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Class fixture type 'FixtureAcceptanceTests+ThrowingCtorFixture' threw in its constructor", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithThrowingFixtureDisposeResultsInFailedTest()
=======
		public static async ValueTask TestClassWithThrowingFixtureDisposeResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestClassCleanupFailure>("FixtureAcceptanceTests+ClassFixture+ClassWithThrowingFixtureDispose");
#else
			var messages = await RunAsync<ITestClassCleanupFailure>(typeof(ClassWithThrowingFixtureDispose));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Class fixture type 'FixtureAcceptanceTests+ThrowingDisposeFixture' threw in Dispose", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureDataIsPassedToConstructorAndAvailableViaContext()
=======
		public static async ValueTask FixtureDataIsPassedToConstructorAndAvailableViaContext()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+ClassFixture+FixtureSpy");
#else
			var messages = await RunAsync<ITestPassed>(typeof(FixtureSpy));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithDefaultParameter()
=======
		public static async ValueTask TestClassWithDefaultParameter()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync("FixtureAcceptanceTests+ClassFixture+ClassWithDefaultCtorArg");
#else
			var messages = await RunAsync(typeof(ClassWithDefaultCtorArg));
#endif

			Assert.Single(messages.OfType<ITestPassed>());
			var starting = Assert.Single(messages.OfType<ITestStarting>());
			Assert.Equal("FixtureAcceptanceTests+ClassFixture+ClassWithDefaultCtorArg.TheTest", starting.TestDisplayName);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithOptionalParameter()
=======
		public static async ValueTask TestClassWithOptionalParameter()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync("FixtureAcceptanceTests+ClassFixture+ClassWithOptionalCtorArg");
#else
			var messages = await RunAsync(typeof(ClassWithOptionalCtorArg));
#endif

			Assert.Single(messages.OfType<ITestPassed>());
			var starting = Assert.Single(messages.OfType<ITestStarting>());
			Assert.Equal("FixtureAcceptanceTests+ClassFixture+ClassWithOptionalCtorArg.TheTest", starting.TestDisplayName);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithParamsParameter()
=======
		public static async ValueTask TestClassWithParamsParameter()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync("FixtureAcceptanceTests+ClassFixture+ClassWithParamsArg");
#else
			var messages = await RunAsync(typeof(ClassWithParamsArg));
#endif

			Assert.Single(messages.OfType<ITestPassed>());
			var starting = Assert.Single(messages.OfType<ITestStarting>());
			Assert.Equal("FixtureAcceptanceTests+ClassFixture+ClassWithParamsArg.TheTest", starting.TestDisplayName);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask ClassFixtureCanAcceptIMessageSink()
=======
		public static async ValueTask ClassFixtureCanAcceptIMessageSink()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+ClassFixture+ClassWithMessageSinkFixture");
#else
			var messages = await RunForResultsAsync(typeof(ClassWithMessageSinkFixture));
#endif

			var passed = Assert.Single(messages.OfType<TestPassedWithMetadata>());
			Assert.Equal("FixtureAcceptanceTests+ClassFixture+ClassWithMessageSinkFixture.MessageSinkWasInjected", passed.Test.TestDisplayName);
		}

		// https://github.com/xunit/xunit/issues/3371
		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureWithAllSkippedTestsIsNotCreated()
=======
		public static async ValueTask FixtureWithAllSkippedTestsIsNotCreated()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+ClassFixture+ClassWithSkippedTests");
#else
			var messages = await RunForResultsAsync(typeof(ClassWithSkippedTests));
#endif

			var message = Assert.Single(messages);
			var skipped = Assert.IsType<TestSkippedWithMetadata>(message);
			Assert.Equal("FixtureAcceptanceTests+ClassFixture+ClassWithSkippedTests.Skipped", skipped.Test.TestDisplayName);
		}

		// https://github.com/xunit/xunit/issues/3371
		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureWithAllSkippedTestsIsNotCreated_WithConstructor()
=======
		public static async ValueTask FixtureWithAllSkippedTestsIsNotCreated_WithConstructor()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+ClassFixture+ClassWithSkippedTests_WithConstructor");
#else
			var messages = await RunForResultsAsync(typeof(ClassWithSkippedTests_WithConstructor));
#endif

			var message = Assert.Single(messages);
			var skipped = Assert.IsType<TestSkippedWithMetadata>(message);
			Assert.Equal("FixtureAcceptanceTests+ClassFixture+ClassWithSkippedTests_WithConstructor.Skipped", skipped.Test.TestDisplayName);
		}
	}

	public partial class CollectionFixture : AcceptanceTestV3
	{
		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithExtraArgumentToConstructorResultsInFailedTest()
=======
		public static async ValueTask TestClassWithExtraArgumentToConstructorResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+CollectionFixture+ClassWithExtraCtorArg");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithExtraCtorArg));
#endif

			var msg = Assert.Single(messages);
			Assert.Equal(typeof(TestPipelineException).SafeName(), msg.ExceptionTypes.Single());
#if XUNIT_AOT
			Assert.Equal("Test class 'FixtureAcceptanceTests.CollectionFixture.ClassWithExtraCtorArg' had one or more unresolved constructor arguments: int _1, string _3", msg.Messages.Single());
#else
			Assert.Equal("The following constructor parameters did not have matching fixture data: Int32 _1, String _3", msg.Messages.Single());
#endif
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithMissingArgumentToConstructorIsAcceptable()
=======
		public static async ValueTask TestClassWithMissingArgumentToConstructorIsAcceptable()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixture+ClassWithMissingCtorArg");
#else
			var messages = await RunAsync<ITestPassed>(typeof(ClassWithMissingCtorArg));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithThrowingFixtureConstructorResultsInFailedTest()
=======
		public static async ValueTask TestClassWithThrowingFixtureConstructorResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+CollectionFixture+ClassWithThrowingFixtureCtor");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithThrowingFixtureCtor));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Collection fixture type 'FixtureAcceptanceTests+ThrowingCtorFixture' threw in its constructor", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithThrowingCollectionFixtureDisposeResultsInFailedTest()
=======
		public static async ValueTask TestClassWithThrowingCollectionFixtureDisposeResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestCollectionCleanupFailure>("FixtureAcceptanceTests+CollectionFixture+ClassWithThrowingFixtureDispose");
#else
			var messages = await RunAsync<ITestCollectionCleanupFailure>(typeof(ClassWithThrowingFixtureDispose));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Collection fixture type 'FixtureAcceptanceTests+ThrowingDisposeFixture' threw in Dispose", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureDataIsPassedToConstructorAndAvailableViaContext()
=======
		public static async ValueTask FixtureDataIsPassedToConstructorAndAvailableViaContext()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+CollectionFixture+FixtureSpy");
#else
			var messages = await RunForResultsAsync(typeof(FixtureSpy));
#endif

			Assert.Single(messages.OfType<TestPassedWithMetadata>());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureDataIsSameInstanceAcrossClasses()
=======
		public static async ValueTask FixtureDataIsSameInstanceAcrossClasses()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var results = await RunForResultsAsync(["FixtureAcceptanceTests+CollectionFixture+FixtureSaver1", "FixtureAcceptanceTests+CollectionFixture+FixtureSaver2"]);
#else
			var results = await RunForResultsAsync([typeof(FixtureSaver1), typeof(FixtureSaver2)]);
#endif

			Assert.Collection(
				results.OfType<TestPassedWithMetadata>().OrderBy(p => p.Output),
				passed => Assert.Equal("FixtureSaver1: Counter value is 1", passed.Output.Trim()),
				passed => Assert.Equal("FixtureSaver2: Counter value is 1", passed.Output.Trim())
			);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask ClassFixtureOnCollectionDecorationWorks()
=======
		public static async ValueTask ClassFixtureOnCollectionDecorationWorks()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixture+FixtureSpy_ClassFixture");
#else
			var messages = await RunAsync<ITestPassed>(typeof(FixtureSpy_ClassFixture));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask ClassFixtureOnTestClassTakesPrecedenceOverClassFixtureOnCollection()
=======
		public static async ValueTask ClassFixtureOnTestClassTakesPrecedenceOverClassFixtureOnCollection()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixture+ClassWithCountedFixture");
#else
			var messages = await RunAsync<ITestPassed>(typeof(ClassWithCountedFixture));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask CollectionFixtureCanAcceptIMessageSink()
=======
		public static async ValueTask CollectionFixtureCanAcceptIMessageSink()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+CollectionFixture+ClassWithMessageSinkFixture");
#else
			var messages = await RunForResultsAsync(typeof(ClassWithMessageSinkFixture));
#endif

			var passed = Assert.Single(messages.OfType<TestPassedWithMetadata>());
			Assert.Equal("FixtureAcceptanceTests+CollectionFixture+ClassWithMessageSinkFixture.MessageSinkWasInjected", passed.Test.TestDisplayName);
		}

		// https://github.com/xunit/xunit/issues/3371
		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureWithAllSkippedTestsIsNotCreated()
=======
		public static async ValueTask FixtureWithAllSkippedTestsIsNotCreated()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+CollectionFixture+ClassWithSkippedTests");
#else
			var messages = await RunForResultsAsync(typeof(ClassWithSkippedTests));
#endif

			var message = Assert.Single(messages);
			var skipped = Assert.IsType<TestSkippedWithMetadata>(message);
			Assert.Equal("FixtureAcceptanceTests+CollectionFixture+ClassWithSkippedTests.Skipped", skipped.Test.TestDisplayName);
		}

		// https://github.com/xunit/xunit/issues/3371
		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureWithAllSkippedTestsIsNotCreated_WithConstructor()
=======
		public static async ValueTask FixtureWithAllSkippedTestsIsNotCreated_WithConstructor()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+CollectionFixture+ClassWithSkippedTests_WithConstructor");
#else
			var messages = await RunForResultsAsync(typeof(ClassWithSkippedTests_WithConstructor));
#endif

			var message = Assert.Single(messages);
			var skipped = Assert.IsType<TestSkippedWithMetadata>(message);
			Assert.Equal("FixtureAcceptanceTests+CollectionFixture+ClassWithSkippedTests_WithConstructor.Skipped", skipped.Test.TestDisplayName);
		}
	}

	public partial class CollectionFixtureByTypeArgument : AcceptanceTestV3
	{
		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithExtraArgumentToConstructorResultsInFailedTest()
=======
		public static async ValueTask TestClassWithExtraArgumentToConstructorResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+CollectionFixtureByTypeArgument+ClassWithExtraCtorArg");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithExtraCtorArg));
#endif

			var msg = Assert.Single(messages);
			Assert.Equal(typeof(TestPipelineException).SafeName(), msg.ExceptionTypes.Single());
#if XUNIT_AOT
			Assert.Equal("Test class 'FixtureAcceptanceTests.CollectionFixtureByTypeArgument.ClassWithExtraCtorArg' had one or more unresolved constructor arguments: int _1, string _3", msg.Messages.Single());
#else
			Assert.Equal("The following constructor parameters did not have matching fixture data: Int32 _1, String _3", msg.Messages.Single());
#endif
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithMissingArgumentToConstructorIsAcceptable()
=======
		public static async ValueTask TestClassWithMissingArgumentToConstructorIsAcceptable()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunForResultsAsync("FixtureAcceptanceTests+CollectionFixtureByTypeArgument+ClassWithMissingCtorArg");
#else
			var messages = await RunForResultsAsync(typeof(ClassWithMissingCtorArg));
#endif

			Assert.Single(messages.OfType<TestPassedWithMetadata>());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithThrowingFixtureConstructorResultsInFailedTest()
=======
		public static async ValueTask TestClassWithThrowingFixtureConstructorResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+CollectionFixtureByTypeArgument+ClassWithThrowingFixtureCtor");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithThrowingFixtureCtor));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Collection fixture type 'FixtureAcceptanceTests+ThrowingCtorFixture' threw in its constructor", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithThrowingCollectionFixtureDisposeResultsInFailedTest()
=======
		public static async ValueTask TestClassWithThrowingCollectionFixtureDisposeResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestCollectionCleanupFailure>("FixtureAcceptanceTests+CollectionFixtureByTypeArgument+ClassWithThrowingFixtureDispose");
#else
			var messages = await RunAsync<ITestCollectionCleanupFailure>(typeof(ClassWithThrowingFixtureDispose));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Collection fixture type 'FixtureAcceptanceTests+ThrowingDisposeFixture' threw in Dispose", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureDataIsPassedToConstructorAndAvailableViaContext()
=======
		public static async ValueTask FixtureDataIsPassedToConstructorAndAvailableViaContext()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixtureByTypeArgument+FixtureSpy");
#else
			var messages = await RunAsync<ITestPassed>(typeof(FixtureSpy));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureDataIsSameInstanceAcrossClasses()
=======
		public static async ValueTask FixtureDataIsSameInstanceAcrossClasses()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var results = await RunForResultsAsync(["FixtureAcceptanceTests+CollectionFixtureByTypeArgument+FixtureSaver1", "FixtureAcceptanceTests+CollectionFixtureByTypeArgument+FixtureSaver2"]);
#else
			var results = await RunForResultsAsync([typeof(FixtureSaver1), typeof(FixtureSaver2)]);
#endif

			Assert.Collection(
				results.OfType<TestPassedWithMetadata>().OrderBy(p => p.Output),
				passed => Assert.Equal("FixtureSaver1: Counter value is 1", passed.Output.Trim()),
				passed => Assert.Equal("FixtureSaver2: Counter value is 1", passed.Output.Trim())
			);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask ClassFixtureOnCollectionDecorationWorks()
=======
		public static async ValueTask ClassFixtureOnCollectionDecorationWorks()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixtureByTypeArgument+FixtureSpy_ClassFixture");
#else
			var messages = await RunAsync<ITestPassed>(typeof(FixtureSpy_ClassFixture));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask ClassFixtureOnTestClassTakesPrecedenceOverClassFixtureOnCollection()
=======
		public static async ValueTask ClassFixtureOnTestClassTakesPrecedenceOverClassFixtureOnCollection()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixtureByTypeArgument+ClassWithCountedFixture");
#else
			var messages = await RunAsync<ITestPassed>(typeof(ClassWithCountedFixture));
#endif

			Assert.Single(messages);
		}
	}

#if !NETFRAMEWORK

	public partial class CollectionFixtureGeneric : AcceptanceTestV3
	{
		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithExtraArgumentToConstructorResultsInFailedTest()
=======
		public static async ValueTask TestClassWithExtraArgumentToConstructorResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+CollectionFixtureGeneric+ClassWithExtraCtorArg");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithExtraCtorArg));
#endif

			var msg = Assert.Single(messages);
			Assert.Equal(typeof(TestPipelineException).SafeName(), msg.ExceptionTypes.Single());
#if XUNIT_AOT
			Assert.Equal("Test class 'FixtureAcceptanceTests.CollectionFixtureGeneric.ClassWithExtraCtorArg' had one or more unresolved constructor arguments: int _1, string _3", msg.Messages.Single());
#else
			Assert.Equal("The following constructor parameters did not have matching fixture data: Int32 _1, String _3", msg.Messages.Single());
#endif
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithMissingArgumentToConstructorIsAcceptable()
=======
		public static async ValueTask TestClassWithMissingArgumentToConstructorIsAcceptable()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixtureGeneric+ClassWithMissingCtorArg");
#else
			var messages = await RunAsync<ITestPassed>(typeof(ClassWithMissingCtorArg));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithThrowingFixtureConstructorResultsInFailedTest()
=======
		public static async ValueTask TestClassWithThrowingFixtureConstructorResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestFailed>("FixtureAcceptanceTests+CollectionFixtureGeneric+ClassWithThrowingFixtureCtor");
#else
			var messages = await RunAsync<ITestFailed>(typeof(ClassWithThrowingFixtureCtor));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Collection fixture type 'FixtureAcceptanceTests+ThrowingCtorFixture' threw in its constructor", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask TestClassWithThrowingCollectionFixtureDisposeResultsInFailedTest()
=======
		public static async ValueTask TestClassWithThrowingCollectionFixtureDisposeResultsInFailedTest()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestCollectionCleanupFailure>("FixtureAcceptanceTests+CollectionFixtureGeneric+ClassWithThrowingFixtureDispose");
#else
			var messages = await RunAsync<ITestCollectionCleanupFailure>(typeof(ClassWithThrowingFixtureDispose));
#endif

			var msg = Assert.Single(messages);
			Assert.Collection(
				msg.ExceptionTypes,
				exceptionTypeName => Assert.Equal(typeof(TestPipelineException).SafeName(), exceptionTypeName),
				exceptionTypeName => Assert.Equal(typeof(DivideByZeroException).SafeName(), exceptionTypeName)
			);
			Assert.Equal("Collection fixture type 'FixtureAcceptanceTests+ThrowingDisposeFixture' threw in Dispose", msg.Messages.First());
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureDataIsPassedToConstructor()
=======
		public static async ValueTask FixtureDataIsPassedToConstructor()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixtureGeneric+FixtureSpy");
#else
			var messages = await RunAsync<ITestPassed>(typeof(FixtureSpy));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask FixtureDataIsSameInstanceAcrossClasses()
=======
		public static async ValueTask FixtureDataIsSameInstanceAcrossClasses()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var results = await RunForResultsAsync(["FixtureAcceptanceTests+CollectionFixtureGeneric+FixtureSaver1", "FixtureAcceptanceTests+CollectionFixtureGeneric+FixtureSaver2"]);
#else
			var results = await RunForResultsAsync([typeof(FixtureSaver1), typeof(FixtureSaver2)]);
#endif

			Assert.Collection(
				results.OfType<TestPassedWithMetadata>().OrderBy(p => p.Output),
				passed => Assert.Equal("FixtureSaver1: Counter value is 1", passed.Output.Trim()),
				passed => Assert.Equal("FixtureSaver2: Counter value is 1", passed.Output.Trim())
			);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask ClassFixtureOnCollectionDecorationWorks()
=======
		public static async ValueTask ClassFixtureOnCollectionDecorationWorks()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixtureGeneric+FixtureSpy_ClassFixture");
#else
			var messages = await RunAsync<ITestPassed>(typeof(FixtureSpy_ClassFixture));
#endif

			Assert.Single(messages);
		}

		[Fact]
<<<<<<< HEAD
		public async ValueTask ClassFixtureOnTestClassTakesPrecedenceOverClassFixtureOnCollection()
=======
		public static async ValueTask ClassFixtureOnTestClassTakesPrecedenceOverClassFixtureOnCollection()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
#if XUNIT_AOT
			var messages = await RunAsync<ITestPassed>("FixtureAcceptanceTests+CollectionFixtureGeneric+ClassWithCountedFixture");
#else
			var messages = await RunAsync<ITestPassed>(typeof(ClassWithCountedFixture));
#endif

			Assert.Single(messages);
		}
	}

#endif
}
