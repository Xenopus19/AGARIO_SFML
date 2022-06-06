using System;
using SFML.System;

namespace Agario;
public static class Extentions
{
    public static Vector2f Normalize(this Vector2f vector)
    {
        return vector * (1 / vector.Length());
    }

    public static float Length(this Vector2f vector)
    {
        float squareLength = vector.X*vector.X + vector.Y*vector.Y;

        return MathF.Sqrt(squareLength);
    }

    public static bool AlmostEqual(this Vector2f vector, Vector2f vector2, float oversight)
    {
        bool AlmostEquals = vector.X.AlmostEqual(vector2.X, oversight) && vector.Y.AlmostEqual(vector2.Y, oversight);
        Console.WriteLine(AlmostEquals);
        return AlmostEquals;
    }

    public static bool AlmostEqual(this float num1, float num2, float oversight)
    {
        return MathF.Abs(num1 - num2) <= oversight;
    }
}

public static class AgarioRandom
{
    private static Random r = new();

    public static int NextInt(int MaxValue)
    {
        return r.Next(MaxValue);
    }
}
