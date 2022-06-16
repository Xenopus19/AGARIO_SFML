using System;
using SFML.System;

namespace Agario.AdditionalTools;
public static class VectorExtentions
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

    

    public static bool PointIsInsideField(this Vector2f point)
    {
        return point.X > 0 && point.X <= Game.WINDOW_X && point.Y > 0 && point.Y <= Game.WINDOW_Y;
    }
}


