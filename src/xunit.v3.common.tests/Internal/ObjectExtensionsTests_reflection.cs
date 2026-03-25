using Xunit;

public static class ObjectExtensionsTests
{
	public static class AsValueTask
	{
		[Fact]
		public static void NullValue()
		{
			var result = ObjectExtensions.AsValueTask(null);

			Assert.Null(result);
		}

		[Fact]
		public static void NonTaskValue()
		{
			var result = ObjectExtensions.AsValueTask(42);

			Assert.Null(result);
		}

		[Fact]
		public static async ValueTask ValueTaskValue()
		{
			var task = new ValueTask<int>(42);

			var result = ObjectExtensions.AsValueTask(task);

			Assert.True(result.HasValue);
			Assert.Equal(42, await result.Value);
		}

		[Fact]
		public static async ValueTask TaskValue()
		{
			var task = Task.FromResult(42);

			var result = ObjectExtensions.AsValueTask(task);

			Assert.True(result.HasValue);
			Assert.Equal(42, await result.Value);
		}
	}
}
