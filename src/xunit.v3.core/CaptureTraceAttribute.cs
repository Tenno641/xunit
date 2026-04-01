#pragma warning disable CA1812  // CaptureTraceImpl is instantiated as an assembly fixture

using System.Diagnostics;
using Xunit.v3;

namespace Xunit;

/// <summary>
/// Captures <see cref="Trace"/> and <see cref="Debug"/> output and reports it to the
/// test output helper.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
public sealed partial class CaptureTraceAttribute
{
	sealed partial class CaptureTraceImpl : IDisposable
	{
		readonly TraceCaptureTestOutputWriter writer;

		/// <summary/>
		public CaptureTraceImpl() =>
			writer = new TraceCaptureTestOutputWriter(TestContextAccessor.Instance);

		/// <summary/>
		public void Dispose() =>
			writer.SafeDispose();
	}
}
