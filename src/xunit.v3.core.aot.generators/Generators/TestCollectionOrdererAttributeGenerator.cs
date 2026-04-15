using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class TestCollectionOrdererAttributeGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.TestCollectionOrdererAttribute, "RegisterAssemblyTestCollectionOrdererFactory")
{
	protected override bool ValidateImplementationType(INamedTypeSymbol type) =>
		type.ImplementsInterface(Types.Xunit.v3.ITestCollectionOrderer);
}

[Generator(LanguageNames.CSharp)]
public class TestCollectionOrdererAttributeOfTGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.TestCollectionOrdererAttribute + "`1", "RegisterAssemblyTestCollectionOrdererFactory")
{ }
