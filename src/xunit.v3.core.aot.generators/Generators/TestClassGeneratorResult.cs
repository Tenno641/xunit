using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

public class TestClassGeneratorResult(GeneratorSyntaxContext context) :
	XunitGeneratorResult(context.SemanticModel, context.Node), IEquatable<TestClassGeneratorResult?>
{
	public CodeGenTestClassRegistration? TestClass { get; set; }

	public required string TestClassType { get; set; }

	public Dictionary<string, (CodeGenTestMethodRegistration TestMethod, List<string> TestCaseFactories)> TestMethods = [];

	public override bool Equals(object? obj) =>
		Equals(obj as TestClassGeneratorResult);

	public bool Equals(TestClassGeneratorResult? other) =>
		other is not null &&
		base.Equals(other) &&
		ComparerHelper.Equals(TestClass, other.TestClass) &&
		ComparerHelper.Equals(TestClassType, other.TestClassType) &&
		ComparerHelper.Equals(TestMethods, other.TestMethods);

	public override int GetHashCode() =>
		Hasher.Extend(base.GetHashCode()).With(TestClass).With(TestClassType).With(TestMethods);
}
