using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

/// <summary>
/// A source generator which converts a single assembly attribute into an engine init attribute with
/// registration code.
/// </summary>
/// <param name="fullyQualifiedAttributeTypeName">The fully qualified attribute name
/// (e.g., <c>"Xunit.TestFrameworkAttribute"</c>)</param>
/// <param name="factoryRegistrationMethod">The factory registration method name off <c>RegisteredEngineConfig</c>
/// (e.g., <c>"RegisterTestFrameworkFactory"</c>). This is required if you don't override <see cref="GetRegistration"/>,
/// but can be omitted if you do.</param>
/// <remarks>
/// This generator converts:<br />
/// <br />
/// <c>[assembly: Attribute(typeof(Implementation)]</c><br />
/// <br />
/// into an engine initialization attribute that calls:<br />
/// <br />
/// <c>RegisteredEngineConfig.FactoryProperty = () => new Implementation();</c>
/// </remarks>
public abstract class AssemblyFactoryAttributeGeneratorBase(
	string fullyQualifiedAttributeTypeName,
	string? factoryRegistrationMethod = null) :
		XunitAttributeGenerator<AssemblyFactoryAttributeGeneratorBase.GeneratorResult>(fullyQualifiedAttributeTypeName)
{
	protected override sealed void CreateSource(
		SourceProductionContext context,
		GeneratorResult result)
	{
		if (result is null || result.Factory is null)
			return;

		AddInitAttribute(context, result, GetRegistration(result));
	}

	protected virtual string? GetFactory(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result)
	{
		Guard.ArgumentNotNull(type);
		Guard.ArgumentNotNull(result);

		if (type.HasParameterlessPublicCtor(out var ctor) &&
			ValidateImplementationType(type))
		{
			var factory = CodeGenRegistration.ToObjectFactory(type, ctor);
			if (factory is not null)
				return $"() => {factory}";
		}

		return null;
	}

	protected virtual string GetRegistration(GeneratorResult result) =>
		$"global::Xunit.v3.RegisteredEngineConfig.{Guard.ArgumentNotNull(factoryRegistrationMethod)}({Guard.ArgumentNotNull(result).Factory});";

	protected override sealed GeneratorResult? Transform(
		GeneratorAttributeSyntaxContext context,
		CancellationToken cancellationToken)
	{
		if (context.TargetSymbol is not IAssemblySymbol)
			return null;

		var result = new GeneratorResult(context);

		var attribute = context.Attributes.FirstOrDefault();
		if (attribute is not null)
		{
			var type = GetTypeArgument(attribute);
			if (type is not null)
			{
				var location = attribute.ApplicationSyntaxReference.Location;
				var factory = GetFactory(type, location, result);
				if (factory is not null)
				{
					result.Type = type.ToCSharp();
					result.Factory = factory;
				}
			}
		}

		return result;
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

	protected virtual bool ValidateImplementationType(INamedTypeSymbol type) =>
		true;

	public sealed class GeneratorResult(GeneratorAttributeSyntaxContext context) :
		XunitGeneratorResult(context.SemanticModel, context.TargetNode), IEquatable<GeneratorResult?>
	{
		public string? Factory { get; set; }

		public string? Type { get; set; }

		public override bool Equals(object? obj) =>
			Equals(obj as GeneratorResult);

		public bool Equals(GeneratorResult? other) =>
			other is not null &&
			base.Equals(other) &&
			ComparerHelper.Equals(Factory, other.Factory) &&
			ComparerHelper.Equals(Type, other.Type);

		public override int GetHashCode() =>
			Hasher.Extend(base.GetHashCode()).With(Factory).With(Type);
	}
}
