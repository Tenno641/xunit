using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

public class TestFrameworkAttributeGeneratorBase(string fullyQualifiedAttributeTypeName) :
	AssemblyFactoryAttributeGeneratorBase(fullyQualifiedAttributeTypeName, "RegisterTestFrameworkFactory")
{
	static readonly HashSet<string> StringTypes = ["string", "string?"];

	protected override string? GetFactory(
		INamedTypeSymbol type,
		Location? location,
		GeneratorResult result)
	{
		Guard.ArgumentNotNull(type);

		if (!ValidateImplementationType(type))
			return null;

		// First check for a ctor that takes a string/string?
		var ctor = type.Constructors.FirstOrDefault(c =>
			!c.IsStatic
				&& c.DeclaredAccessibility == Accessibility.Public
				&& c.Parameters.Length == 1
				&& StringTypes.Contains(c.Parameters[0].Type.ToCSharp())
		);
		if (ctor is not null)
			return $"configFileName => new {type.ToCSharp()}(configFileName)";

		// Fall back to a parameterless ctor
		ctor = type.Constructors.FirstOrDefault(c => !c.IsStatic && c.DeclaredAccessibility == Accessibility.Public && c.Parameters.Length == 0);
		if (ctor is not null)
			return $"configFileName => new {type.ToCSharp()}()";

		return null;
	}
}

[Generator(LanguageNames.CSharp)]
public class TestFrameworkAttributeGenerator() :
	TestFrameworkAttributeGeneratorBase(Types.Xunit.TestFrameworkAttribute)
{
	protected override bool ValidateImplementationType(INamedTypeSymbol type) =>
		type.ImplementsInterface(Types.Xunit.v3.ITestFramework);
}

[Generator(LanguageNames.CSharp)]
public class TestFrameworkAttributeOfTGenerator() :
	TestFrameworkAttributeGeneratorBase(Types.Xunit.TestFrameworkAttribute + "`1")
{ }
