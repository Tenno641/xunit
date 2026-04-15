using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class TestPipelineStartupAttributeGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.v3.TestPipelineStartupAttribute, "RegisterTestPipelineStartupFactory")
{
	protected override bool ValidateImplementationType(INamedTypeSymbol type) =>
		type.ImplementsInterface(Types.Xunit.v3.ITestPipelineStartup);
}

[Generator(LanguageNames.CSharp)]
public class TestPipelineStartupAttributeOfTGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.v3.TestPipelineStartupAttribute + "`1", "RegisterTestPipelineStartupFactory")
{ }
