using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class TraitAttributeGenerator() :
	XunitAttributeGenerator<TraitAttributeGenerator.GeneratorResult>(Types.Xunit.TraitAttribute)
{
	protected override void CreateSource(
		SourceProductionContext context,
		GeneratorResult result)
	{
		if (result is null || result.NameValuePairs.Count == 0)
			return;

		AddInitAttribute(
			context,
			result,
			string.Join(
				"\n",
				result.NameValuePairs.Select(nvp => $"global::Xunit.v3.RegisteredEngineConfig.RegisterAssemblyTrait({nvp.Name.Quoted()}, {nvp.Value.Quoted()});")
			)
		);
	}

	protected override GeneratorResult? Transform(
		GeneratorAttributeSyntaxContext context,
		CancellationToken cancellationToken)
	{
		if (context.TargetSymbol is not IAssemblySymbol)
			return null;

		var result = new GeneratorResult(context);

		foreach (var attribute in context.Attributes)
		{
			if (attribute.ConstructorArguments.Length != 2)
				continue;

			if (attribute.ConstructorArguments[0].Kind != TypedConstantKind.Primitive
					|| attribute.ConstructorArguments[1].Kind != TypedConstantKind.Primitive
					|| attribute.ConstructorArguments[0].Value is not string name
					|| attribute.ConstructorArguments[1].Value is not string value)
				continue;

			result.NameValuePairs.Add((name, value));
		}

		return result;
	}

	public class GeneratorResult(GeneratorAttributeSyntaxContext context) :
		XunitGeneratorResult(context.SemanticModel, context.TargetNode), IEquatable<GeneratorResult?>
	{
		public List<(string Name, string Value)> NameValuePairs { get; } = [];

		public override bool Equals(object? obj) =>
			Equals(obj as GeneratorResult);

		public bool Equals(GeneratorResult? other) =>
			other is not null &&
			base.Equals(other) &&
			ComparerHelper.Equals(NameValuePairs, other.NameValuePairs);

		public override int GetHashCode() =>
			Hasher.Extend(base.GetHashCode()).With(NameValuePairs);
	}
}
