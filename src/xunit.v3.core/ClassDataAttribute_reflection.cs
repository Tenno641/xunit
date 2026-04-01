#pragma warning disable CA1813  // This attribute is unsealed because it's an extensibility point

using System.Collections;
using System.Reflection;
using Xunit.Sdk;

namespace Xunit;

partial class ClassDataAttribute
{
	/// <inheritdoc/>
	protected override ITheoryDataRow ConvertDataRow(object dataRow)
	{
		Guard.ArgumentNotNull(dataRow);

		try
		{
			return base.ConvertDataRow(dataRow);
		}
		catch (ArgumentException)
		{
			throw new ArgumentException(
				string.Format(
					CultureInfo.CurrentCulture,
					"Class '{0}' yielded an item of type '{1}' which is not an 'object?[]', 'Xunit.ITheoryDataRow' or 'System.Runtime.CompilerServices.ITuple'",
					Class.FullName,
					dataRow?.GetType().SafeName()
				),
				nameof(dataRow)
			);
		}
	}

	/// <inheritdoc/>
	public override async ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(
		MethodInfo testMethod,
		DisposalTracker disposalTracker)
	{
		Guard.ArgumentNotNull(disposalTracker);

		var classInstance = Activator.CreateInstance(Class);
		disposalTracker.Add(classInstance);

		if (classInstance is IAsyncLifetime classLifetime)
			await classLifetime.InitializeAsync();

		if (classInstance is IEnumerable dataItems)
		{
			var result = new List<ITheoryDataRow>();

			foreach (var dataItem in dataItems)
				if (dataItem is not null)
					result.Add(ConvertDataRow(dataItem));

			return result.CastOrToReadOnlyCollection();
		}

		if (classInstance is IAsyncEnumerable<object?> asyncDataItems)
		{
			var result = new List<ITheoryDataRow>();

			await foreach (var dataItem in asyncDataItems)
				if (dataItem is not null)
					result.Add(ConvertDataRow(dataItem));

			return result.CastOrToReadOnlyCollection();
		}

		throw new ArgumentException(
			string.Format(
				CultureInfo.CurrentCulture,
				"'{0}' must implement one of the following interfaces to be used as ClassData:{1}- IEnumerable<ITheoryDataRow>{1}- IEnumerable<object[]>{1}- IAsyncEnumerable<ITheoryDataRow>{1}- IAsyncEnumerable<object[]>",
				Class.FullName,
				Environment.NewLine
			)
		);
	}

	/// <inheritdoc/>
	public override bool SupportsDiscoveryEnumeration() =>
		!typeof(IDisposable).IsAssignableFrom(Class) && !typeof(IAsyncDisposable).IsAssignableFrom(Class);
}

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="ClassDataAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class ClassDataAttribute<TClass>() :
	ClassDataAttribute(typeof(TClass))
{ }
