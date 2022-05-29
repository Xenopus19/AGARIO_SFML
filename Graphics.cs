using System;
using SFML.Graphics;


public static class Graphics
{
    private static Random random = new();
    public static Color GetRandomColor()
    {
        return new Color((uint)random.Next(32));
    }
}
