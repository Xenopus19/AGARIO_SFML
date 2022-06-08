using System;
using SFML.Graphics;
using SFML.System;
using Agario.AdditionalTools;

namespace Agario.GameObjects;
public class Food : IDrawable, ICollidable
{
	public Action<Food> OnEaten;

	private CircleShape sprite;

	private int PowerAmount;

	public Food()
	{
		PowerAmount = 2;
		sprite = new();
		sprite.Position = Game.GetRandomPosition();
		InitGraphics();
	}

	public int GetEaten()
    {
		if(OnEaten!=null)
        {
			OnEaten.Invoke(this);
			return PowerAmount;
        }
		return 0;
    }

	public void OnCollisionEnter(ICollidable collided)
    {
    
	}

	public int GetPower() => PowerAmount;

	public FloatRect GetCollider() => sprite.GetGlobalBounds();

	public void Draw(RenderWindow window)
	{
		sprite.Draw(window, RenderStates.Default);
	}

    private void InitGraphics()
    {
		sprite.FillColor = Color.White;
		sprite.Radius = 20;
		sprite.Texture = Graphics.FoodTexture;
    }
}
