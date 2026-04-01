namespace Xunit;

partial class CaptureTraceAttribute : AssemblyFixtureAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CaptureTraceAttribute"/> class.
	/// </summary>
	public CaptureTraceAttribute() :
		base(typeof(CaptureTraceImpl))
	{ }
}
