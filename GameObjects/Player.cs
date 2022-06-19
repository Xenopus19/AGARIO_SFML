using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario.Controllers;
using Agario.AdditionalTools;

namespace Agario.GameObjects;


public class Player : DeletableObject, ICollidable, IUpdatable, IDrawable
{
	public static Player CurrentPlayer;

	private const int RADIUS_COEF = 10;

	private float Speed;
	private int Power;

	private CircleShape sprite;

	public Player()
	{
		sprite = new();
		sprite.Position = AgarioRandom.GetRandomPosition();
		Speed = 0.5f;
		Power = 1;
		InitGraphics();
	}
	public void Update()
    {
		CheckHp();
    }

	public Vector2f Position => sprite.Position;

	public void GetDamage(int Damage)
    {
		ChangePower(-Damage);
    }

	

	private void CheckHp()
    {
		if (Power <= 0) InvokeDeleteEvent();
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

	public void MakeShot(Vector2f clickedPosition)
    {
		Bullet bullet  = Game.Instantiate<Bullet>();
		bullet.Init(this, -(Position-clickedPosition).Normalize());
    }


	private void OnCollision(ICollidable collidable)
    {
		if(collidable is Food)
        {
			Food food = (Food)collidable;
			ChangePower(food.GetEaten());
        }
		else if(collidable is Player)
        {
			Player player = (Player)collidable;
			if(Power > player.Power)
            {
				ChangePower(player.Power);
				player.GetEaten();
            }
        }
    }

	private void ChangePower(int DeltaPower)
    {
		Power += DeltaPower;
		CalculateNewStats();
    }

	public FloatRect GetCollider()
    {
		return sprite.GetGlobalBounds();
    }

	public void Move(Vector2f Direction)
    {
		Vector2f newPos = sprite.Position + Direction * Speed * AgarioTime.DeltaTime;

		if(newPos.PointIsInsideField())
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
		sprite.Origin = new Vector2f(sprite.Radius, sprite.Radius);
		Speed /= Power;
    }
	public void GetEaten()
    {
		InvokeDeleteEvent();
    }

	public void SetOutlineThickness(int thickness)
    {
		sprite.OutlineThickness = thickness;
    }
}
