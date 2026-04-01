using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

[Generator(LanguageNames.CSharp)]
public class CaptureTraceAttributeGenerator() :
	CaptureOutputGeneratorBase(Types.Xunit.CaptureTraceAttribute, "global::" + Types.Xunit.CaptureTraceAttribute + ".CaptureTraceImpl")
{ }
