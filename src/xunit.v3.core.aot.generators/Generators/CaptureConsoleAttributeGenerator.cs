using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class CaptureConsoleAttributeGenerator() :
	CaptureOutputGeneratorBase(Types.Xunit.CaptureConsoleAttribute, "global::" + Types.Xunit.CaptureConsoleAttribute + ".CaptureConsoleImpl")
{ }
