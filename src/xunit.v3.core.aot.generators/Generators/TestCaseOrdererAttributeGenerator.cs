using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class TestCaseOrdererAttributeGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.TestCaseOrdererAttribute, "RegisterAssemblyTestCaseOrdererFactory")
{
	protected override bool ValidateImplementationType(INamedTypeSymbol type) =>
		type.ImplementsInterface(Types.Xunit.v3.ITestCaseOrderer);
}

[Generator(LanguageNames.CSharp)]
public class TestCaseOrdererAttributeOfTGenerator() :
	AssemblyFactoryAttributeGeneratorBase(Types.Xunit.TestCaseOrdererAttribute + "`1", "RegisterAssemblyTestCaseOrdererFactory")
{
	protected override bool ValidateImplementationType(INamedTypeSymbol type) =>
		type.ImplementsInterface(Types.Xunit.v3.ITestCaseOrderer);
}
