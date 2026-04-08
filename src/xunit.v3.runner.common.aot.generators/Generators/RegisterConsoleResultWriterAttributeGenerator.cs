using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class RegisterResultConsoleWriterAttributeGenerator() :
	IDAndTypeGenerator(
		Types.Xunit.Runner.Common.RegisterConsoleResultWriterAttribute,
		(id, type) => $@"global::Xunit.Runner.Common.RegisteredRunnerConfig.RegisterConsoleResultWriter(""{id}"", new {type}());")
{
	protected override bool ValidateType(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result) =>
			type.ImplementsInterface(Types.Xunit.Runner.Common.IConsoleResultWriter);
}

[Generator(LanguageNames.CSharp)]
public class RegisterResultConsoleWriterAttributeOfTGenerator() :
	IDAndTypeGenerator(
		Types.Xunit.Runner.Common.RegisterConsoleResultWriterAttribute + "`1",
		(id, type) => $@"global::Xunit.Runner.Common.RegisteredRunnerConfig.RegisterConsoleResultWriter(""{id}"", new {type}());")
{
	protected override bool ValidateType(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result) =>
			type.ImplementsInterface(Types.Xunit.Runner.Common.IConsoleResultWriter);
}
