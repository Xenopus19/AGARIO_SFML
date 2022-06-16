using System;

namespace Agario.AdditionalTools;
public static class FloatExtentions
{
    public static bool AlmostEqual(this float num1, float num2, float oversight)
    {
        return MathF.Abs(num1 - num2) <= oversight;
    }
}
