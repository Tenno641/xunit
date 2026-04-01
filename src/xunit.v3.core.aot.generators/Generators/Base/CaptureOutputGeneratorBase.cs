using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

public class CaptureOutputGeneratorBase(
	string fullyQualifiedAttributeTypeName,
	string fixtureTypeName) :
		XunitAttributeGenerator<XunitGeneratorResult>(fullyQualifiedAttributeTypeName)
{
	protected override void CreateSource(
		SourceProductionContext context,
		XunitGeneratorResult result)
	{
		if (result is null)
			return;

		AddInitAttribute(
			context,
			result,
			$"global::Xunit.v3.RegisteredEngineConfig.RegisterAssemblyFixtureFactory(typeof({fixtureTypeName}), async () => new {fixtureTypeName}());"
		);
	}

	protected override XunitGeneratorResult? Transform(
		GeneratorAttributeSyntaxContext context,
		CancellationToken cancellationToken)
	{
		if (context.TargetSymbol is not IAssemblySymbol)
			return null;

		return new XunitGeneratorResult(context.SemanticModel, context.TargetNode);
	}
}
