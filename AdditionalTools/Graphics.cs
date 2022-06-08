using System;
using SFML.Graphics;

namespace Agario;
public static class Graphics
{
    public static Texture FoodTexture;

    private static List<Texture> PlayerTextures = new List<Texture>();

    public static void LoadTextures()
    {
        for(int i = 0; i < 3; i++)
        {
            Texture playerTexture = new("player" + i + ".png");
            PlayerTextures.Add(playerTexture);
        }

        FoodTexture = new("food.png");
    }

    public static Texture GetRandomPlayerTexture()
    {
        return PlayerTextures[AgarioRandom.NextInt(PlayerTextures.Count)];
    }
}
