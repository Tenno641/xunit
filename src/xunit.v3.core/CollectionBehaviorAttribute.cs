using Xunit.v3;

namespace Xunit;

/// <summary>
/// Used to declare the default test collection behavior for the assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
public sealed partial class CollectionBehaviorAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CollectionBehaviorAttribute" /> class.
	/// Uses the default collection behavior (<see cref="CollectionBehavior.CollectionPerClass"/>).
	/// </summary>
	public CollectionBehaviorAttribute()
	{ }

#pragma warning disable CA1019  // We don't want a property accessor for CollectionBehavior because it's just a type selector

	/// <summary>
	/// Initializes a new instance of the <see cref="CollectionBehaviorAttribute" /> class
	/// with the given built-in collection behavior.
	/// </summary>
	/// <param name="collectionBehavior">The collection behavior for the assembly.</param>
	public CollectionBehaviorAttribute(CollectionBehavior collectionBehavior) =>
		// This is an attribute constructor; throwing here would be wrong, so we just always fall back to the default
		CollectionFactoryType = collectionBehavior switch
		{
			CollectionBehavior.CollectionPerClass => typeof(CollectionPerClassTestCollectionFactory),
			CollectionBehavior.CollectionPerAssembly => typeof(CollectionPerAssemblyTestCollectionFactory),
			_ => null,
		};

#pragma warning restore CA1019

	public partial CollectionBehaviorAttribute(Type collectionFactoryType) =>
		CollectionFactoryType = collectionFactoryType;
}

/// <summary>
/// Used to declare the default test collection behavior for the assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
public sealed partial class CollectionBehaviorAttribute<TCollectionFactory> : Attribute
{ }
