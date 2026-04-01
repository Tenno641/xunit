namespace Xunit.Runner.Common;

partial class RegisterResultWriterAttribute : IRegisterConsoleResultWriterAttribute, IRegisterMicrosoftTestingPlatformResultWriterAttribute
{ }

/// <remarks>
/// .NET Framework does not support generic attributes. Please use the non-generic <see cref="RegisterResultWriterAttribute"/>
/// when targeting .NET Framework.
/// </remarks>
partial class RegisterResultWriterAttribute<TResultWriter> : IRegisterConsoleResultWriterAttribute, IRegisterMicrosoftTestingPlatformResultWriterAttribute
{ }
