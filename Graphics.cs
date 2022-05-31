using System;
using SFML.Graphics;


public static class Graphics
{
    public static Texture FoodTexture;

    private static List<Texture> PlayerTextures = new List<Texture>();

    private static Random random = new();

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
        return PlayerTextures[random.Next(PlayerTextures.Count)];
    }
}
