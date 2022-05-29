using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario;

public class Game
{
	public const int WINDOW_X = 1600;
	public const int WINDOW_Y = 900;
	public const string GAME_NAME = "Шарики-каннибалы";

	private RenderWindow window;

	private List<IUpdatable> updatableList;
	private List<ICollidable> collidableList;
	private List<IDrawable> drawableList;
	public Game()
	{
		window = new (new(WINDOW_X, WINDOW_Y), GAME_NAME);
		window.SetFramerateLimit(60);
		collidableList = new ();
		drawableList = new ();
		updatableList = new ();
	}

	public void Begin()
    {
		Spawn<Ball>();
		while(window.IsOpen)
        {
			GameCycle();
        }
    }

	public void GameCycle()
    {
		Time.UpdateDeltaTime();

		UpdateObjects();
		CheckCollision();

		DrawObjects();
    }

	private void DrawObjects()
    {
		window.Clear();

		foreach(IDrawable drawable in drawableList)
        {
			drawable.Draw(window);
        }

		window.Display();
	}

	private void UpdateObjects()
    {
		foreach (IUpdatable updatable in updatableList)
		{
			updatable.Update();
		}
	}

	private void CheckCollision()
    {
		foreach (ICollidable collidable in collidableList)
		{
			foreach (ICollidable collidable1 in collidableList)
			{
				if (collidable == collidable1) continue;

				collidable.CheckColiision(collidable1);
			}
		}
	}

	private void Spawn<Type>() where Type : new()
    {
		Type gameObject = new ();

		TryAddToCollection<IDrawable>(gameObject, drawableList);
		TryAddToCollection<IUpdatable>(gameObject, updatableList);
		TryAddToCollection<ICollidable>(gameObject, collidableList);
	}

	private void TryAddToCollection<T>(object gameObject, List<T> collection)
    {
		if (gameObject is T)
			collection.Add((T)gameObject);
	}
}
