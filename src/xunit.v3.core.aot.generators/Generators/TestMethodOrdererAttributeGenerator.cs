using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class TestMethodOrdererAttributeGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.TestMethodOrdererAttribute, "RegisterAssemblyTestMethodOrdererFactory")
{
	protected override bool ValidateImplementationType(INamedTypeSymbol type) =>
		type.ImplementsInterface(Types.Xunit.v3.ITestMethodOrderer);
}

[Generator(LanguageNames.CSharp)]
public class TestMethodOrdererAttributeOfTGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.TestMethodOrdererAttribute + "`1", "RegisterAssemblyTestMethodOrdererFactory")
{ }
