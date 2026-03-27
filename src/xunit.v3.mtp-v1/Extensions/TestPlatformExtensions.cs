using Microsoft.Testing.Platform.Extensions.OutputDevice;
using Microsoft.Testing.Platform.OutputDevice;

namespace Xunit.MicrosoftTestingPlatform;

internal static class TestPlatformExtensions
{
	public static Task DisplayAsync(this IOutputDevice output, IOutputDeviceDataProducer producer, IOutputDeviceData data, CancellationToken cancellationToken) =>
		output.DisplayAsync(producer, data);
}
