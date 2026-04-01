using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

public abstract class RegisterRunnerReporterAttributeGeneratorBase(string fullyQualifiedAttributeTypeName) :
	XunitAttributeGenerator<RegisterRunnerReporterAttributeGeneratorBase.GeneratorResult>(fullyQualifiedAttributeTypeName)
{
	protected override void CreateSource(
		SourceProductionContext context,
		GeneratorResult result)
	{
		if (result is null || result.RunnerReporters.Count == 0)
			return;

		AddInitAttribute(
			context, result,
			string.Join(
				"\r\n",
				result
					.RunnerReporters
					.WhereNotNull()
					.Select(type => $"global::Xunit.Runner.Common.RegisteredRunnerConfig.RegisterRunnerReporter(new {type}());")
			)
		);
	}

	protected virtual INamedTypeSymbol? GetTypeArgument(AttributeData attribute) =>
		FullyQualifiedAttributeTypeName.EndsWith("`1", StringComparison.Ordinal)
			? GetTypeArgumentGeneric(attribute)
			: GetTypeArgumentNonGeneric(attribute);

	protected static INamedTypeSymbol? GetTypeArgumentGeneric(AttributeData attribute)
	{
		if (attribute?.AttributeClass is not { } attributeType)
			return null;

		return
			attributeType.TypeArguments.Length == 1
				? attributeType.TypeArguments[0] as INamedTypeSymbol
				: null;
	}

	protected static INamedTypeSymbol? GetTypeArgumentNonGeneric(AttributeData attribute) =>
		attribute?.ConstructorArguments.Length == 1
			? attribute.ConstructorArguments[0].Value as INamedTypeSymbol
			: null;

	protected override GeneratorResult? Transform(
		GeneratorAttributeSyntaxContext context,
		CancellationToken cancellationToken)
	{
		if (context.TargetSymbol is not IAssemblySymbol)
			return null;

		var result = new GeneratorResult(context);

		foreach (var attribute in context.Attributes)
		{
			var reporterType = GetTypeArgument(attribute);
			if (reporterType is not null)
			{
				var location = attribute.ApplicationSyntaxReference.Location;
				if (EnsureParameterlessPublicCtor(reporterType, location, result, out var _) &&
					EnsureImplementsInterface(reporterType, location, result, Types.Xunit.Runner.Common.IRunnerReporter))
					result.RunnerReporters.Add(reporterType.ToString());
			}
		}

		return result;
	}

	public sealed class GeneratorResult(GeneratorAttributeSyntaxContext context) :
		XunitGeneratorResult(context.SemanticModel, context.TargetNode)
	{
		public List<string?> RunnerReporters = [];
	}
}

[Generator(LanguageNames.CSharp)]
public class RegisterRunnerReporterAttributeGenerator() :
	RegisterRunnerReporterAttributeGeneratorBase(Types.Xunit.Runner.Common.RegisterRunnerReporterAttribute)
{ }


[Generator(LanguageNames.CSharp)]
public class RegisterRunnerReporterAttributeOfTGenerator() :
	RegisterRunnerReporterAttributeGeneratorBase(Types.Xunit.Runner.Common.RegisterRunnerReporterAttribute + "`1")
{ }
