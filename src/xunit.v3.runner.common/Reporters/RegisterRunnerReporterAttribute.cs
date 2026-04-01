namespace Xunit.Runner.Common;

/// <summary>
/// Used to decorate xUnit.net test assemblies to indicate the availability of a custom
/// runner reporter.
/// </summary>
/// <param name="runnerReporterType">The type of the runner reporter to register. The type
/// must implement <see cref="IRunnerReporter"/>.</param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed partial class RegisterRunnerReporterAttribute(Type runnerReporterType) : Attribute
{
	/// <summary>
	/// Gets the type of the runner reporter to be registered.
	/// </summary>
	public Type RunnerReporterType => Guard.ArgumentNotNull(runnerReporterType);
}

/// <summary>
/// Used to decorate xUnit.net test assemblies to indicate the availability of a custom
/// runner reporter.
/// </summary>
/// <typeparam name="TReporter">The type of the runner reporter to register</typeparam>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed partial class RegisterRunnerReporterAttribute<TReporter> : Attribute
	where TReporter : IRunnerReporter
{
	/// <summary>
	/// Gets the type of the runner reporter to be registered.
	/// </summary>
	public Type RunnerReporterType => typeof(TReporter);
}
