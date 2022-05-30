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

	private List<object> objectsToDelete;

	private float FoodCooldown;
	private float PassedCooldown;

	private int PlayersAmount;
	public Game()
	{
		window = new (new(WINDOW_X, WINDOW_Y), GAME_NAME);
		window.SetFramerateLimit(60);
		collidableList = new ();
		drawableList = new ();
		updatableList = new ();
		objectsToDelete = new();

		FoodCooldown = 1000;
		PassedCooldown = 0;

		PlayersAmount = 5;
	}

	public static Vector2f GetRandomPosition()
    {
		Random random = new Random();

		return new Vector2f(random.Next(WINDOW_X), random.Next(WINDOW_Y));
    }

	public void Begin()
    {
		SpawnPlayers();
		while (window.IsOpen)
        {
			GameCycle();
        }
    }

	private void SpawnPlayers()
    {
		Player notAI = Spawn<Player>();
		notAI.ToggleBotOrPlayer();
		notAI.OnEaten += AddToDespawnList;

		for (int i = 0; i < PlayersAmount; i++)
			Spawn<Player>().OnEaten += AddToDespawnList;
    }

	public void GameCycle()
    {
		Time.UpdateDeltaTime();

		UpdateObjects();
		CheckCollision();
		TrySpawnFood();

		DrawObjects();

		ClearDespawnList();
    }

	private void TrySpawnFood()
    {
		PassedCooldown += Time.DeltaTime;

		if(PassedCooldown>=FoodCooldown)
        {
			Spawn<Food>().OnEaten+=AddToDespawnList;
			PassedCooldown = 0;
        }
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

				collidable.CheckCollision(collidable1);
			}
		}
	}

	private Type Spawn<Type>() where Type : new()
    {
		Type gameObject = new ();

		TryAddToCollection<IDrawable>(gameObject, drawableList);
		TryAddToCollection<IUpdatable>(gameObject, updatableList);
		TryAddToCollection<ICollidable>(gameObject, collidableList);

		return gameObject;
	}
	private void TryAddToCollection<T>(object gameObject, List<T> collection)
	{
		if (gameObject is T)
			collection.Add((T)gameObject);
	}

	private void ClearDespawnList()
	{
		foreach (object ToDespawn in objectsToDelete)
		{
			Despawn(ToDespawn);
		}

		objectsToDelete.Clear();
	}

	private void AddToDespawnList(object ToDespawn)
    {
		objectsToDelete.Add(ToDespawn);
    }

	private void Despawn(object ToDespawn)
    {
		TryRemoveFromCollection<IDrawable>(ToDespawn, drawableList);
		TryRemoveFromCollection<IUpdatable>(ToDespawn, updatableList);
		TryRemoveFromCollection<ICollidable>(ToDespawn, collidableList);
    }

	private void TryRemoveFromCollection<T>(object ToRemove, List<T> collection)
    {
		if (!(ToRemove is T)) return;

		if (collection.Contains((T)ToRemove))
			collection.Remove((T)ToRemove);
	}

	
}
