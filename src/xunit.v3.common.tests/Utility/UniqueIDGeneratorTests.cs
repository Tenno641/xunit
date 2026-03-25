using Xunit;
using Xunit.Sdk;

public static class UniqueIDGeneratorTests
{
	public static class Compute
	{
		[Fact]
		public static void GuardClause()
		{
			using var generator = new UniqueIDGenerator();

			var ex = Record.Exception(() => generator.Add(null!));

			var anex = Assert.IsType<ArgumentNullException>(ex);
			Assert.Equal("value", anex.ParamName);
		}

		[Fact]
		public static void NoData()
		{
			using var generator = new UniqueIDGenerator();

			var result = generator.Compute();

			Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", result);
		}

		[Fact]
		public static void SingleString()
		{
			using var generator = new UniqueIDGenerator();
			generator.Add("Hello, world!");

			var result = generator.Compute();

			Assert.Equal("5450bb49d375ba935c1fe9c4dc48775d7d343fb97f22f07f8950f34a75a2708f", result);
		}

		[Fact]
		public static void CannotAddAfterCompute()
		{
			using var generator = new UniqueIDGenerator();
			generator.Compute();

			var ex = Record.Exception(() => generator.Add("Hello, world!"));

			Assert.IsType<ObjectDisposedException>(ex);
			Assert.StartsWith("Cannot use UniqueIDGenerator after you have called Compute or Dispose", ex.Message);
		}

		[Fact]
		public static void CannotComputeTwice()
		{
			using var generator = new UniqueIDGenerator();
			generator.Compute();

			var ex = Record.Exception(() => generator.Compute());

			Assert.IsType<ObjectDisposedException>(ex);
			Assert.StartsWith("Cannot use UniqueIDGenerator after you have called Compute or Dispose", ex.Message);
		}

		[Fact]
		public static void CannotAddAfterDipose()
		{
			using var generator = new UniqueIDGenerator();
			generator.Dispose();

			var ex = Record.Exception(() => generator.Add("Hello, world!"));

			Assert.IsType<ObjectDisposedException>(ex);
			Assert.StartsWith("Cannot use UniqueIDGenerator after you have called Compute or Dispose", ex.Message);
		}

		[Fact]
		public static void CannotComputeAfterDispose()
		{
			using var generator = new UniqueIDGenerator();
			generator.Dispose();

			var ex = Record.Exception(() => generator.Compute());

			Assert.IsType<ObjectDisposedException>(ex);
			Assert.StartsWith("Cannot use UniqueIDGenerator after you have called Compute or Dispose", ex.Message);
		}
	}

	public static class ForAssembly
	{
		[Fact]
		public static void GuardClause()
		{
			var ex = Record.Exception(() => UniqueIDGenerator.ForAssembly(null!, "config-path"));

			var argnEx = Assert.IsType<ArgumentNullException>(ex);
			Assert.Equal("assemblyPath", argnEx.ParamName);
		}

		[Theory]
		[InlineData("asm-path", null, "9b101eb6f7a9ca48b696d43c4384ce51c3b1522ca5d82cddc04900177ee4824f")]
		[InlineData("asm-path", "config-path", "87f1ea729573561e318de0a470397143c37511bed90420cb0ad4536b0e149c68")]
		public static void SuccessCases(
			string assemblyPath,
			string? configFilePath,
			string expected)
		{
			var actual = UniqueIDGenerator.ForAssembly(assemblyPath, configFilePath);

			Assert.Equal(expected, actual);
		}
	}
}
