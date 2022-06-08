using System;
using SFML.System;

namespace Agario.AdditionalTools;

public static class AgarioTime
{
    public static float DeltaTime;

    private static Clock clock = new();

    public static void UpdateDeltaTime()
    {
        DeltaTime = clock.ElapsedTime.AsMilliseconds();
        clock.Restart();
    }
}
