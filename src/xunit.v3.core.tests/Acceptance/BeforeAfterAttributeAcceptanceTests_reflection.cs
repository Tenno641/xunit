using Xunit;
using Xunit.v3;

<<<<<<< HEAD
public class BeforeAfterAttributeAcceptanceTests
{
	[Fact]
	public void TestCollectionComesAfterTestAssembly()
=======
public static class BeforeAfterAttributeAcceptanceTests
{
	[Fact]
	public static void TestCollectionComesAfterTestAssembly()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var assemblyAttributes = new[] { new AssemblyBeforeAfter() };

		var result = ExtensibilityPointFactory.GetCollectionBeforeAfterTestAttributes(typeof(MyCollection), assemblyAttributes);

		Assert.Collection(
			result,
			attr => Assert.IsType<AssemblyBeforeAfter>(attr),
			attr => Assert.IsType<CollectionBeforeAfter>(attr)
		);
	}

	[Fact]
<<<<<<< HEAD
	public void TestClassComesAfterTestCollection()
=======
	public static void TestClassComesAfterTestCollection()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var collectionAttributes = new[] { new CollectionBeforeAfter() };

		var result = ExtensibilityPointFactory.GetClassBeforeAfterTestAttributes(typeof(MyTestClass), collectionAttributes);

		Assert.Collection(
			result,
			attr => Assert.IsType<CollectionBeforeAfter>(attr),
			attr => Assert.IsType<ClassBeforeAfter>(attr)
		);
	}

	[Fact]
<<<<<<< HEAD
	public void TestMethodComesAfterTestClass()
=======
	public static void TestMethodComesAfterTestClass()
>>>>>>> b7f7500bf174aa126fc8f0708a47425cff08f940
	{
		var classAttributes = new[] { new ClassBeforeAfter() };
		var methodInfo = typeof(MyTestClass).GetMethod(nameof(MyTestClass.TestMethod));
		Assert.NotNull(methodInfo);

		var result = ExtensibilityPointFactory.GetMethodBeforeAfterTestAttributes(methodInfo, classAttributes);

		Assert.Collection(
			result,
			attr => Assert.IsType<ClassBeforeAfter>(attr),
			attr => Assert.IsType<MethodBeforeAfter>(attr)
		);
	}

	[CollectionBeforeAfter]
	class MyCollection { }

#pragma warning disable CA1822 // Mark members as static

	[ClassBeforeAfter]
	class MyTestClass
	{
		[MethodBeforeAfter]
		public void TestMethod() { }
	}

#pragma warning restore CA1822 // Mark members as static

	class AssemblyBeforeAfter : BeforeAfterTestAttribute { }
	class CollectionBeforeAfter : BeforeAfterTestAttribute { }
	class ClassBeforeAfter : BeforeAfterTestAttribute { }
	class MethodBeforeAfter : BeforeAfterTestAttribute { }
}
