using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

public abstract class XunitAttributeGenerator<TResult>(string fullyQualifiedAttributeTypeName) :
	XunitGenerator
		where TResult : XunitGeneratorResult
{
	protected string FullyQualifiedAttributeTypeName { get; } =
		Guard.ArgumentNotNull(fullyQualifiedAttributeTypeName);

	protected abstract void CreateSource(
		SourceProductionContext context,
		TResult result);

	protected override sealed void Initialize(
		IncrementalGeneratorInitializationContext context,
		IncrementalValueProvider<string> projectPath,
		IncrementalValueProvider<INamedTypeSymbol> objectType)
	{
		var result =
			context
				.SyntaxProvider
				.ForAttributeWithMetadataName(fullyQualifiedAttributeTypeName, ValidateAttribute, Transform)
				.WhereNotNull()
				.Combine(projectPath)
				.Select((pair, _) =>
				{
					pair.Left.ProjectPath = pair.Right;
					return pair.Left;
				});

		context.RegisterSourceOutput(result, CreateSource);
	}

	protected abstract TResult? Transform(
		GeneratorAttributeSyntaxContext context,
		CancellationToken cancellationToken);

	protected virtual bool ValidateAttribute(
		SyntaxNode syntaxNode,
		CancellationToken cancellationToken) =>
			true;
}
