namespace Xunit;

partial class CaptureConsoleAttribute : AssemblyFixtureAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CaptureConsoleAttribute"/> class.
	/// </summary>
	public CaptureConsoleAttribute() :
		base(typeof(CaptureConsoleImpl))
	{ }
}
