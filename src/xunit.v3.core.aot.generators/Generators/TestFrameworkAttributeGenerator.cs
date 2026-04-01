using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class TestFrameworkAttributeGenerator() :
	TestFrameworkAttributeGeneratorBase(Types.Xunit.TestFrameworkAttribute)
{ }

[Generator(LanguageNames.CSharp)]
public class TestFrameworkAttributeOfTGenerator() :
	TestFrameworkAttributeGeneratorBase(Types.Xunit.TestFrameworkAttribute + "`1")
{ }
