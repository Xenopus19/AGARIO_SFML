using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario;

public struct Controls
{
	public Keyboard.Key UpKey;
	public Keyboard.Key LeftKey;
	public Keyboard.Key DownKey;
	public Keyboard.Key RightKey;

	public Controls()
	{
		UpKey = Keyboard.Key.W;
		LeftKey = Keyboard.Key.A;
		DownKey = Keyboard.Key.S;
		RightKey = Keyboard.Key.D;
	}

	public Controls(Keyboard.Key up, Keyboard.Key left, Keyboard.Key down, Keyboard.Key right)
    {
		UpKey = up;
		LeftKey = left;	
		DownKey = down;
		RightKey = right;
    }
}
public class Ball : ICollidable, IUpdatable, IDrawable
{
	private const int RADIUS_COEF = 10;

	private bool IsBot;
	private Controls controls;
	private float Speed;
	private float Power;

	private Random random;

	private CircleShape sprite;

	public Ball()
	{
		Speed = 0.5f;
		controls = new();
		IsBot = false;
		Power = 1;
		InitGraphics();
	}
	public void Update()
    {
		Move();
    }

	public void Draw(RenderWindow window)
    {
		sprite.Draw(window, RenderStates.Default);
    }

	public void CheckCollision(ICollidable collider)
    {
		if(GetCollider().Intersects(collider.GetCollider()))
        {
			OnCollision(collider);
        }
    }

	private void OnCollision(ICollidable collidable)
    {
		Console.WriteLine("Ball collided");
    }

	public FloatRect GetCollider()
    {
		return sprite.GetGlobalBounds();
    }

	private void Move()
    {
		Vector2i Direction;
		if (IsBot)
			Direction = GetRandomDirection();
		else
			Direction = GetInputDirection();

		sprite.Position += (Vector2f)Direction * Speed * Time.DeltaTime;
    }

	private Vector2i GetRandomDirection()
    {
		Vector2i direction = new(0, 0);

		direction.X = GenerateRandomCoordinate();
		direction.Y = GenerateRandomCoordinate();

		return direction;
    }

	private int GenerateRandomCoordinate()
    {
		int coordinate = 0;
		if (random.Next(10) % 2 == 0)
		{
			coordinate = 1;
			if (random.Next(10) % 2 == 0)
			{
				coordinate = -1;
			}
		}
		return coordinate;
	}

	private Vector2i GetInputDirection()
    {
		Vector2i dir = new Vector2i();

		if(Keyboard.IsKeyPressed(controls.UpKey))
		    dir.Y = -1;
		if (Keyboard.IsKeyPressed(controls.DownKey))
			dir.Y = 1;
		if (Keyboard.IsKeyPressed(controls.LeftKey))
			dir.X = -1;
		if (Keyboard.IsKeyPressed(controls.RightKey))
			dir.X = 1;

		return dir;
	}

	private void InitGraphics()
    {
		sprite = new();
		sprite.Radius = RADIUS_COEF * Power;
		sprite.FillColor = Color.White;
    }
}
