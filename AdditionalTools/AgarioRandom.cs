using System;
using SFML.System;

namespace Agario.AdditionalTools;
public static class AgarioRandom
{
    private static Random r = new();

    public static int NextInt(int MaxValue)
    {
        return r.Next(MaxValue);
    }

    public static Vector2f GetRandomPosition()
    {
        return new Vector2f(AgarioRandom.NextInt(Game.WindowX), AgarioRandom.NextInt(Game.WindowY));
    }
}
