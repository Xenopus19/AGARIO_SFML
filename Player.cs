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
public class Player : ICollidable, IUpdatable, IDrawable
{
	public Action<Player> OnEaten;

	private const int RADIUS_COEF = 5;

	private bool IsBot;
	private Controls controls;
	private float Speed;
	private int Power;

	private Random random;

	private CircleShape sprite;

	public Player()
	{
		sprite = new();
		sprite.Position = Game.GetRandomPosition();
		Speed = 0.5f;
		random = new();
		controls = new();
		IsBot = true;
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

	public int GetPower => Power;

	public void ToggleBotOrPlayer() => IsBot = !IsBot;

	private void OnCollision(ICollidable collidable)
    {
		if(collidable is Food)
        {
			Food food = (Food)collidable;
			AddPower(food.GetEaten());
        }
		else if(collidable is Player)
        {
			Player player = (Player)collidable;
			if(Power > player.Power)
            {
				AddPower(player.Power);
				player.GetEaten();
            }
        }
    }

	private void AddPower(int DeltaPower)
    {
		Power += DeltaPower;
		CalculateNewStats();
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
		CalculateNewStats();
		sprite.FillColor = Color.White;
		sprite.Texture = Graphics.GetRandomPlayerTexture();
    }

	private void CalculateNewStats()
    {
		sprite.Radius = RADIUS_COEF * Power + 5;
		Speed /= Power;
    }

	public void GetEaten()
    {
		if (OnEaten != null)
			OnEaten.Invoke(this);
    }
}
