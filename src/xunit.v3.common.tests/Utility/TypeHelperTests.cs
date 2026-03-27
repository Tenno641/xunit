using Xunit;
using Xunit.Sdk;

<<<<<<< HEAD
public partial class TypeHelperTests
{
	public class ConversionAcceptanceTests
	{
		[Theory]
		[InlineData("{5B21E154-15EB-4B1E-BC30-127E8A41ECA1}")]
		public void GuidAcceptanceTest(Guid actual)
=======
public static partial class TypeHelperTests
{
	public static class ConversionAcceptanceTests
	{
		[Theory]
		[InlineData("{5B21E154-15EB-4B1E-BC30-127E8A41ECA1}")]
		public static void GuidAcceptanceTest(Guid actual)
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var expected = Guid.Parse("{5B21E154-15EB-4B1E-BC30-127E8A41ECA1}");

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("2017-11-3 16:48")]
		public static void DateTimeAcceptanceTest(DateTime actual)
		{
			var expected = DateTime.Parse("2017-11-3 16:48", CultureInfo.InvariantCulture);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("2017-11-3 16:48")]
		public static void DateTimeOffsetAcceptanceTest(DateTimeOffset actual)
		{
			var expected = DateTimeOffset.Parse("2017-11-3 16:48", CultureInfo.InvariantCulture);

			Assert.Equal(expected, actual);
		}
	}

<<<<<<< HEAD
	public class TryConvert
=======
	public static class TryConvert
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		[Theory]
		[InlineData("{5B21E154-15EB-4B1E-BC30-127E8A41ECA1}")]
		[InlineData("4EBCD32C-A2B8-4600-9E72-3873347E285C")]
		[InlineData("39A3B4C85FEF43A988EB4BB4AC4D4103")]
		[InlineData("{5b21e154-15eb-4b1e-bc30-127e8a41eca1}")]
		[InlineData("4ebcd32c-a2b8-4600-9e72-3873347e285c")]
		[InlineData("39a3b4c85fef43a988eb4bb4ac4d4103")]
<<<<<<< HEAD
		public void ConvertsStringToGuid(string text)
=======
		public static void ConvertsStringToGuid(string text)
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var guid = Guid.Parse(text);

			var success = TypeHelper.TryConvert<Guid>(text, out var result);

			Assert.True(success);
			Assert.Equal(guid, result);
		}

		[Theory]
		[InlineData("2017-11-3")]
		[InlineData("2017-11-3 16:48")]
		[InlineData("16:48")]
<<<<<<< HEAD
		public void ConvertsStringToDateTime(string text)
=======
		public static void ConvertsStringToDateTime(string text)
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var dateTime = DateTime.Parse(text, CultureInfo.InvariantCulture);

			var success = TypeHelper.TryConvert<DateTime>(text, out var result);

			Assert.True(success);
			Assert.Equal(dateTime, result);
		}

		[Theory]
		[InlineData("2017-11-3")]
		[InlineData("2017-11-3 16:48")]
		[InlineData("16:48")]
<<<<<<< HEAD
		public void ConvertsStringToDateTimeOffset(string text)
=======
		public static void ConvertsStringToDateTimeOffset(string text)
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var dateTimeOffset = DateTimeOffset.Parse(text, CultureInfo.InvariantCulture);

			var success = TypeHelper.TryConvert<DateTimeOffset>(text, out var result);

			Assert.True(success);
			Assert.Equal(dateTimeOffset, result);
		}
	}

<<<<<<< HEAD
	public class TryConvertNullable
=======
	public static class TryConvertNullable
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		[Theory]
		[InlineData("{5B21E154-15EB-4B1E-BC30-127E8A41ECA1}")]
		[InlineData("4EBCD32C-A2B8-4600-9E72-3873347E285C")]
		[InlineData("39A3B4C85FEF43A988EB4BB4AC4D4103")]
		[InlineData("{5b21e154-15eb-4b1e-bc30-127e8a41eca1}")]
		[InlineData("4ebcd32c-a2b8-4600-9e72-3873347e285c")]
		[InlineData("39a3b4c85fef43a988eb4bb4ac4d4103")]
<<<<<<< HEAD
		public void ConvertsStringToGuid(string text)
=======
		public static void ConvertsStringToGuid(string text)
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var guid = Guid.Parse(text);

			var success = TypeHelper.TryConvertNullable<Guid>(text, out var result);

			Assert.True(success);
			Assert.Equal(guid, result);
		}

		[Theory]
		[InlineData("2017-11-3")]
		[InlineData("2017-11-3 16:48")]
		[InlineData("16:48")]
<<<<<<< HEAD
		public void ConvertsStringToDateTime(string text)
=======
		public static void ConvertsStringToDateTime(string text)
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var dateTime = DateTime.Parse(text, CultureInfo.InvariantCulture);

			var success = TypeHelper.TryConvertNullable<DateTime>(text, out var result);

			Assert.True(success);
			Assert.Equal(dateTime, result);
		}

		[Theory]
		[InlineData("2017-11-3")]
		[InlineData("2017-11-3 16:48")]
		[InlineData("16:48")]
<<<<<<< HEAD
		public void ConvertsStringToDateTimeOffset(string text)
=======
		public static void ConvertsStringToDateTimeOffset(string text)
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
		{
			var dateTimeOffset = DateTimeOffset.Parse(text, CultureInfo.InvariantCulture);

			var success = TypeHelper.TryConvertNullable<DateTimeOffset>(text, out var result);

			Assert.True(success);
			Assert.Equal(dateTimeOffset, result);
		}
	}
}
