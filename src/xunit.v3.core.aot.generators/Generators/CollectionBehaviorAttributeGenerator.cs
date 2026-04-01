using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class CollectionBehaviorAttributeGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.CollectionBehaviorAttribute, "RegisterTestCollectionFactoryFactory")
{
	protected override string? GetFactory(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result)
	{
		Guard.ArgumentNotNull(type);
		Guard.ArgumentNotNull(result);

		return
			EnsureImplementsInterface(type, location, result, Types.Xunit.v3.ICodeGenTestCollectionFactory) &&
			EnsureConstructorParameters(type, location, result, [Types.Xunit.v3.ICodeGenTestAssembly])
				? $"(assembly) => new {type.ToCSharp()}(assembly)"
				: null;
	}
}

[Generator(LanguageNames.CSharp)]
public class CollectionBehaviorAttributeOfTGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.CollectionBehaviorAttribute + "`1", "RegisterTestCollectionFactoryFactory")
{
	protected override string? GetFactory(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result)
	{
		Guard.ArgumentNotNull(type);
		Guard.ArgumentNotNull(result);

		return
			EnsureImplementsInterface(type, location, result, Types.Xunit.v3.ICodeGenTestCollectionFactory) &&
			EnsureConstructorParameters(type, location, result, [Types.Xunit.v3.ICodeGenTestAssembly])
				? $"(assembly) => new {type.ToCSharp()}(assembly)"
				: null;
	}
}
