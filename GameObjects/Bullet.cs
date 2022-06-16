using System;
using SFML.Graphics;
using SFML.System;
using Agario.AdditionalTools;

namespace Agario.GameObjects;
public class Bullet : DeletableObject, ICollidable, IDrawable, IUpdatable
{
	private CircleShape sprite;

	private Player owner;

	private float speed;
	private float power;

	private Vector2f direction;
	public Bullet()
	{
		InitGraphics();
		speed = 0.7f;
		power = 2;
	}

	public void Init(Player owner, Vector2f dir)
    {
		this.owner = owner;
		direction = dir;
		sprite.Position = owner.Position;
    }

	public void Update()
    {
		FlyForward();
		CheckOutOfBorderPos();
    }

	public void Draw(RenderWindow window) => sprite.Draw(window, RenderStates.Default);

	private void FlyForward()
    {
		sprite.Position += direction * speed * AgarioTime.DeltaTime;
    }

	private void CheckOutOfBorderPos()
    {
		if(!sprite.Position.PointIsInsideField())
        {
			InvokeDeleteEvent();
        }
    }

	public FloatRect GetCollider()
    {
		return sprite.GetGlobalBounds();
    }

	public void OnCollision(ICollidable collidable)
    {
		if(collidable is Player player && player != owner)
        {
			player.GetDamage((int)power);
        }
    }

	public void CheckCollision(ICollidable collidable)
    {
		if (GetCollider().Intersects(collidable.GetCollider()))
		{
			OnCollision(collidable);
		}
	}

	private void InitGraphics()
    {
		sprite = new CircleShape(10);
		sprite.FillColor = Color.Red;
    }
}
