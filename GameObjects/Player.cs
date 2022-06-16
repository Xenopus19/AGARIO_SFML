﻿using System;
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

	private Controller controller;

	private CircleShape sprite;

	public Player()
	{
		sprite = new();
		sprite.Position = AgarioRandom.GetRandomPosition();
		Speed = 0.5f;
		BecomeAI();
		Power = 1;
		InitGraphics();
	}
	public void Update()
    {
		controller.GetCommands(this);
		CheckSoulRecast();
		CheckHp();
    }

	public Vector2f Position => sprite.Position;

	public void GetDamage(int Damage)
    {
		ChangePower(-Damage);
    }

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

	private void CheckHp()
    {
		if (Power <= 0) InvokeDeleteEvent();
    }

	public void Draw(RenderWindow window)
    {
		if (controller is PlayerController) sprite.OutlineThickness = 5f;
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
		if (controller is PlayerController)
			BecomeAI();
		else
			BecomePlayer();
    }

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
		Speed /= Power;
    }

	public void GetEaten()
    {
		InvokeDeleteEvent();
    }

	private void BecomePlayer()
    {
		controller = new PlayerController(new());
		CurrentPlayer = this;
    }

	private void BecomeAI()
    {
		controller = new AIController();
    }
}
