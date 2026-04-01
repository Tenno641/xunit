using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class RegisterMicrosoftTestingPlatformResultWriterAttributeGenerator() :
	IDAndTypeGenerator(
		Types.Xunit.Runner.Common.RegisterMicrosoftTestingPlatformResultWriterAttribute,
		(id, type) => $@"global::Xunit.Runner.Common.RegisteredRunnerConfig.RegisterMicrosoftTestingPlatformResultWriter(""{id}"", new {type}());")
{
	protected override bool ValidateType(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result) =>
			EnsureImplementsInterface(type, location, result, Types.Xunit.Runner.Common.IMicrosoftTestingPlatformResultWriter);
}

[Generator(LanguageNames.CSharp)]
public class RegisterMicrosoftTestingPlatformResultWriterAttributeOfTGenerator() :
	IDAndTypeGenerator(
		Types.Xunit.Runner.Common.RegisterMicrosoftTestingPlatformResultWriterAttribute + "`1",
		(id, type) => $@"global::Xunit.Runner.Common.RegisteredRunnerConfig.RegisterMicrosoftTestingPlatformResultWriter(""{id}"", new {type}());")
{
	protected override bool ValidateType(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result) =>
			EnsureImplementsInterface(type, location, result, Types.Xunit.Runner.Common.IMicrosoftTestingPlatformResultWriter);
}
