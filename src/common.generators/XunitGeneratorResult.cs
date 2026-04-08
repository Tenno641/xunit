using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

public class XunitGeneratorResult(
	SemanticModel model,
	SyntaxNode syntaxNode) :
		IEquatable<XunitGeneratorResult>
{
	// Ensure attributes have unique names based on the syntax tree where they're generated from
	readonly string baseSuffix = $"{model.SyntaxTree.FilePath}:{syntaxNode.GetLocation().SourceSpan.Start}".ToCompilerSafeName();

	// Used to decorate type names to make them easier to identify in the generated types list
	// (for example, TestClassGenerator will put the class name into the generated attribute name)
	public string GeneratorSuffix { get; set; } = string.Empty;

	public string ProjectPath { get; set; } = string.Empty;

	public string SafeNameSuffix =>
		GeneratorSuffix + baseSuffix;

	public override bool Equals(object? obj) =>
		Equals(obj as XunitGeneratorResult);

	public bool Equals(XunitGeneratorResult? other) =>
		other is not null &&
		ComparerHelper.Equals(baseSuffix, other.baseSuffix) &&
		ComparerHelper.Equals(GeneratorSuffix, other.GeneratorSuffix) &&
		ComparerHelper.Equals(ProjectPath, other.ProjectPath);

	public override int GetHashCode() =>
		Hasher.Start().With(baseSuffix).With(GeneratorSuffix).With(ProjectPath);
}
