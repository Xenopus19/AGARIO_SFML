using System;
using SFML.Graphics;

namespace Agario;

public interface ICollidable
{
	public void OnCollision(ICollidable collidedObject) { }
	public void CheckCollision(ICollidable collidedObject) { }

	public FloatRect GetCollider() { return new(); }
}

public interface IUpdatable
{
	public void Update() { }
}

public interface IDrawable
{
	public void Draw(RenderWindow window) { }
}

public class DeletableObject
{
	public Action<DeletableObject> OnDestroy;

	protected void InvokeDeleteEvent()
    {
		if(OnDestroy != null)
        {
			OnDestroy.Invoke(this);
        }
    }
}

