using Microsoft.CodeAnalysis;

namespace Xunit.Generators;

public abstract class ClassDataAttributeGeneratorBase(string fullyQualifiedAttributeType) :
	DataAttributeGeneratorBase(fullyQualifiedAttributeType)
{
	protected static void ProcessClassDataAttribute(
		INamedTypeSymbol classSymbol,
		IMethodSymbol methodSymbol,
		AttributeData attribute,
		INamedTypeSymbol classDataType,
		string dataAttributeRegistration,
		GeneratorResult result)
	{
		Guard.ArgumentNotNull(classSymbol);
		Guard.ArgumentNotNull(methodSymbol);
		Guard.ArgumentNotNull(attribute);
		Guard.ArgumentNotNull(classDataType);
		Guard.ArgumentNotNull(dataAttributeRegistration);
		Guard.ArgumentNotNull(result);

		if (classDataType.DeclaredAccessibility != Accessibility.Public || classDataType.IsAbstract)
			return;

		if (!classDataType.Constructors.Any(c => c.DeclaredAccessibility == Accessibility.Public && !c.IsStatic && c.Parameters.Length == 0))
			return;

		var theoryDataInfo = classDataType.GetTheoryDataInfo(result.ObjectType);
		if (theoryDataInfo is null)
			return;

		result.GeneratorSuffix = $"{classSymbol.Name}­­­٠{methodSymbol.Name}٠";

		var foreachAwait = theoryDataInfo.Value.IsAsyncEnumerable ? "await " : "";
		var dataRowAwait = theoryDataInfo.Value.IsTask ? "await " : "";
		var asyncClassDataInit =
			classDataType.Implements(Types.Xunit.IAsyncLifetime)
				? "await ((global::Xunit.IAsyncLifetime)classData).InitializeAsync();\n\t"
				: string.Empty;

		result.Factories.Add(($$"""
			async disposalTracker => {
				var attr = {{dataAttributeRegistration}};
				var dataRows = new global::System.Collections.Generic.List<global::Xunit.ITheoryDataRow>();
				var classData = new {{classDataType.ToCSharp()}}();
				disposalTracker.Add(classData);
				{{asyncClassDataInit}}{{foreachAwait}}foreach (var dataRow in {{dataRowAwait}}classData)
					dataRows.Add(attr.CreateDataRow(dataRow));
				return dataRows;
			}
			""", false));
	}
}
