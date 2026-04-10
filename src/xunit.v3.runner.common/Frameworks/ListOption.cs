namespace Xunit.Runner.Common;

/// <summary>
/// Indicates the kind of list a runner should generate, rather than running tests.
/// </summary>
public enum ListOption
{
	/// <summary>
	/// Lists all the classes in the assembly which contain tests.
	/// </summary>
	Classes = 1,

	/// <summary>
	/// Lists the test cases as discovery objects (only valid for the in-process runner).
	/// </summary>
	Discovery,

	/// <summary>
	/// Lists full metadata about the test discovery.
	/// </summary>
	Full,

	/// <summary>
	/// Lists all the methods in the assembly which contain a test.
	/// </summary>
	Methods,

	/// <summary>
	/// Lists all the tests (as display name) in the assembly.
	/// </summary>
	Tests,

	/// <summary>
	/// Lists all the traits that are generated from the assembly.
	/// </summary>
	Traits,
}

/// <summary>
/// Extension methods for <see cref="ListOption"/>
/// </summary>
public static class ListOptionExtensions
{
	extension(ListOption)
	{
		/// <summary>
		/// Gets the valid values for <see cref="ListOption"/>.
		/// </summary>
		public static HashSet<ListOption> ValidValues =>
		[
			ListOption.Classes,
			ListOption.Discovery,
			ListOption.Full,
			ListOption.Methods,
			ListOption.Tests,
			ListOption.Traits,
		];
	}

	/// <summary>
	/// Determines if the value is a valid enum value.
	/// </summary>
	public static bool IsValid(this ListOption value) =>
		ListOption.ValidValues.Contains(value);
}
