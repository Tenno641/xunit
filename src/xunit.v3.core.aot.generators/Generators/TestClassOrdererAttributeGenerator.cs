using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class TestClassOrdererAttributeGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.TestClassOrdererAttribute, "RegisterAssemblyTestClassOrdererFactory")
{
	protected override bool ValidateImplementationType(INamedTypeSymbol type) =>
		type.ImplementsInterface(Types.Xunit.v3.ITestClassOrderer);
}

[Generator(LanguageNames.CSharp)]
public class TestClassOrdererAttributeOfTGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.TestClassOrdererAttribute + "`1", "RegisterAssemblyTestClassOrdererFactory")
{ }
