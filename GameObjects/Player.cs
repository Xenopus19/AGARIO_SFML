using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario.Controllers;
using Agario.AdditionalTools;

namespace Agario.GameObjects;


public class Player : DeletableObject, ICollidable, IUpdatable, IDrawable
{
	private static Player CurrentPlayer;

	private const int RADIUS_COEF = 10;

	private float Speed;
	private int Power;

	private IController inputController;

	private CircleShape sprite;

	public Player()
	{
		sprite = new();
		sprite.Position = Game.GetRandomPosition();
		Speed = 0.5f;
		BecomeAI();
		Power = 1;
		InitGraphics();
	}
	public void Update()
    {
		Move();
		CheckSoulRecast();
    }

	public Vector2f Position => sprite.Position;

	private void CheckSoulRecast()
    {
		Vector2i mousePos = Mouse.GetPosition();
		if(sprite.GetGlobalBounds().Contains(mousePos.X, mousePos.Y) && CurrentPlayer!=this)
        {
			Console.WriteLine("Soul Recast");
			CurrentPlayer.ToggleBotOrPlayer();
			ToggleBotOrPlayer();
        }
    }

	public void Draw(RenderWindow window)
    {
		if (inputController is PlayerController) sprite.OutlineThickness = 5f;
		else sprite.OutlineThickness = 0;
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

	public void ToggleBotOrPlayer()
    {
		if (inputController is PlayerController)
			BecomeAI();
		else
			BecomePlayer();
    }



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
		Vector2f Direction = inputController.GetDirection();

		Vector2f newPos = sprite.Position + (Vector2f)Direction * Speed * AgarioTime.DeltaTime;

		if(Game.PointIsInsideField(newPos))
			sprite.Position = newPos;
    }


	private void InitGraphics()
    {
		CalculateNewStats();
		sprite.FillColor = Color.White;
		sprite.Texture = Graphics.GetRandomPlayerTexture();
    }

	private void CalculateNewStats()
    {
		sprite.Radius = RADIUS_COEF * Power + 10;
		Speed /= Power;
    }

	public void GetEaten()
    {
		if (OnEaten != null)
			OnEaten.Invoke(this);
    }

	private void BecomePlayer()
    {
		inputController = new PlayerController(new());
		CurrentPlayer = this;
    }

	private void BecomeAI()
    {
		inputController = new AIController();
    }
}
