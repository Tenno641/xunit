namespace Xunit.Runner.Common;

partial class RegisterRunnerReporterAttribute : IRegisterRunnerReporterAttribute
{ }

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="RegisterRunnerReporterAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class RegisterRunnerReporterAttribute<TReporter> : IRegisterRunnerReporterAttribute
{ }
