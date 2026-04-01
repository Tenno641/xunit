using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

public abstract class IDAndTypeGenerator(
	string fullyQualifiedAttributeTypeName,
	Func<string, string, string> perItemInit) :
		XunitAttributeGenerator<IDAndTypeGenerator.GeneratorResult>(fullyQualifiedAttributeTypeName)
{
	protected override sealed void CreateSource(
		SourceProductionContext context,
		GeneratorResult result)
	{
		if (result is null || result.Entries.Count == 0)
			return;

		AddInitAttribute(
			context, result,
			string.Join("\r\n", result.Entries.Where(rw => rw.Type is not null).Select(rw => perItemInit(rw.ID, rw.Type!)))
		);
	}

	protected override sealed GeneratorResult? Transform(
		GeneratorAttributeSyntaxContext context,
		CancellationToken cancellationToken)
	{
		if (context.TargetSymbol is not IAssemblySymbol)
			return null;

		var result = new GeneratorResult(context);

		foreach (var attribute in context.Attributes)
		{
			var (type, id) = GetTypeAndID(attribute);
			if (type is not null)
			{
				var location = attribute.ApplicationSyntaxReference.Location;
				if (EnsureParameterlessPublicCtor(type, location, result, out var _) && ValidateType(type, location, result))
					result.Entries.Add((id, type.ToString()));
			}
		}

		return result;
	}

	protected virtual (INamedTypeSymbol? Type, string ID) GetTypeAndID(AttributeData attribute) =>
		FullyQualifiedAttributeTypeName.EndsWith("`1", StringComparison.Ordinal)
			? GetTypeAndIDGeneric(attribute)
			: GetTypeAndIDNonGeneric(attribute);

	protected static (INamedTypeSymbol? Type, string ID) GetTypeAndIDGeneric(AttributeData attribute)
	{
		if (attribute?.AttributeClass is not { } attributeType)
			return (null, string.Empty);

		return
			attributeType.TypeArguments.Length == 1 &&
			attributeType.TypeArguments[0] is INamedTypeSymbol type &&
			attribute.ConstructorArguments.Length == 1 &&
			attribute.ConstructorArguments[0].Value is string id
				? (type, id)
				: (null, string.Empty);
	}

	protected static (INamedTypeSymbol? Type, string ID) GetTypeAndIDNonGeneric(AttributeData attribute) =>
		Guard.ArgumentNotNull(attribute).ConstructorArguments.Length == 2 &&
		attribute.ConstructorArguments[0].Value is string id &&
		attribute.ConstructorArguments[1].Value is INamedTypeSymbol type
			? (type, id)
			: (null, string.Empty);

	protected virtual bool ValidateType(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result) =>
			true;

	public sealed class GeneratorResult(GeneratorAttributeSyntaxContext context) :
		XunitGeneratorResult(context.SemanticModel, context.TargetNode)
	{
		public List<(string ID, string? Type)> Entries = [];
	}
}
