using Xunit;
using Xunit.v3;

public class BeforeAfterAttributeAcceptanceTests
{
	readonly Dictionary<string, CodeGenTestCollectionRegistration> collectionDefinitions = new()
	{
		[CollectionAttribute.GetCollectionNameForType(typeof(MyCollection))] = new CodeGenTestCollectionRegistration { Type = typeof(MyCollection) }
	};

	[Fact]
	public void TestCollectionComesAfterTestAssembly()
	{
		var testAssembly = Mocks.CodeGenTestAssembly(beforeAfterTestAttributes: [new AssemblyBeforeAfter()], collectionDefinitions: collectionDefinitions);
		var collectionFactory = new CollectionPerClassTestCollectionFactory(testAssembly);

		var collection = collectionFactory.Get(typeof(MyTestClass));

		Assert.Collection(
			collection.BeforeAfterTestAttributes,
			attr => Assert.IsType<AssemblyBeforeAfter>(attr),
			attr => Assert.IsType<CollectionBeforeAfter>(attr)
		);
	}

	[Fact]
	public void TestClassComesAfterTestCollection()
	{
		var testAssembly = Mocks.CodeGenTestAssembly(collectionDefinitions: collectionDefinitions);
		var classRegistration = new CodeGenTestClassRegistration { Class = typeof(MyTestClass) };

		var testClass = classRegistration.GetTestClass(testAssembly);

		Assert.Collection(
			testClass.BeforeAfterTestAttributes,
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
		var testAssembly = Mocks.CodeGenTestAssembly();
		var classRegistration = new CodeGenTestClassRegistration { Class = typeof(MyTestClass) };
		var testClass = classRegistration.GetTestClass(testAssembly);
		var methodRegistration = new CodeGenTestMethodRegistration() { BeforeAfterAttributesFactory = () => [new MethodBeforeAfter()] };

		var testMethod = methodRegistration.GetTestMethod(testClass, "TestMethod");

		Assert.Collection(
			testMethod.BeforeAfterTestAttributes,
			attr => Assert.IsType<ClassBeforeAfter>(attr),
			attr => Assert.IsType<MethodBeforeAfter>(attr)
		);
	}

	[CollectionBeforeAfter]
	class MyCollection { }

#pragma warning disable CA1822 // Mark members as static

	[Collection<MyCollection>]
	[ClassBeforeAfter]
	class MyTestClass
	{ }

#pragma warning restore CA1822 // Mark members as static

	class AssemblyBeforeAfter : BeforeAfterTestAttribute { }
	class CollectionBeforeAfter : BeforeAfterTestAttribute { }
	class ClassBeforeAfter : BeforeAfterTestAttribute { }
	class MethodBeforeAfter : BeforeAfterTestAttribute { }
}
