namespace Xunit.Runner.Common;

/// <summary>
/// Used to decorate xUnit.net test assemblies to indicate the availability of a custom
/// result writer (in Microsoft Testing Platform mode).
/// </summary>
/// <param name="id">The ID of the result writer. Will add command line switches for the writer
/// as <c>"--xunit-result-{ID}"</c> and <c>"--xunit-result-{ID}-filename"</c>.</param>
/// <param name="resultWriterType">The type of the result writer to register. The type must
/// implement <see cref="IMicrosoftTestingPlatformResultWriter"/>.</param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed partial class RegisterMicrosoftTestingPlatformResultWriterAttribute(
	string id,
	Type resultWriterType) :
		Attribute
{
	/// <summary>
	/// Gets the ID of the result writer.
	/// </summary>
	public string ID => Guard.ArgumentNotNull(id);

	/// <summary>
	/// Gets the type of the result writer to register.
	/// </summary>
	public Type ResultWriterType => Guard.ArgumentNotNull(resultWriterType);
}

/// <summary>
/// Used to decorate xUnit.net test assemblies to indicate the availability of a custom
/// result writer (in Microsoft Testing Platform mode).
/// </summary>
/// <param name="id">The ID of the result writer. Will add command line switches for the writer
/// as <c>"--xunit-result-{ID}"</c> and <c>"--xunit-result-{ID}-filename"</c>.</param>
/// <typeparam name="TResultWriter">The type of the result writer to register</typeparam>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed partial class RegisterMicrosoftTestingPlatformResultWriterAttribute<TResultWriter>(string id) : Attribute
	where TResultWriter : IMicrosoftTestingPlatformResultWriter
{
	/// <summary>
	/// Gets the ID of the result writer.
	/// </summary>
	public string ID => Guard.ArgumentNotNull(id);

	/// <summary>
	/// Gets the type of the result writer to register.
	/// </summary>
	public Type ResultWriterType => typeof(TResultWriter);
}
