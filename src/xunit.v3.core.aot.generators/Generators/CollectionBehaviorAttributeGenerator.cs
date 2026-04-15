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
			type.ImplementsInterface(Types.Xunit.v3.ICodeGenTestCollectionFactory) &&
			type.HasConstructorParameters([Types.Xunit.v3.ICodeGenTestAssembly])
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
			type.HasConstructorParameters([Types.Xunit.v3.ICodeGenTestAssembly])
				? $"(assembly) => new {type.ToCSharp()}(assembly)"
				: null;
	}
}
