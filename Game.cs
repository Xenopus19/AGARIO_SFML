using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario.AdditionalTools;
using Agario.GameObjects;
using Agario.Controllers;

namespace Agario;

public class Game
{
	public static int WindowX;
	public static int WindowY;
	public static string WindowName;

	private static Game game;

	private RenderWindow window;

	private AgarioList<IUpdatable> updatableList;
	private AgarioList<ICollidable> collidableList;
	private AgarioList<IDrawable> drawableList;

	private List<DeletableObject> objectsToDelete;
	private List<object> objectsToSpawn;

	private AutomaticSpawner<Food> foodSpawner;
	private AutomaticSpawner<AIController> botSpawner;

	private int BasePlayersAmount;
	public Game()
	{
		game = this;

		
		Graphics.LoadTextures();
		collidableList = new();
		drawableList = new();
		updatableList = new();
		objectsToDelete = new();
		objectsToSpawn = new();


		BasePlayersAmount = 5;
	}

	public static Game GetInstance()
    {
		return game;
    }

	public static T Instantiate<T>() where T : new()
    {
		return game.Spawn<T>();
	}

	private void PostLoad()
    {
		GameSettingsLoader.LoadConfig();
		window = new(new((uint)WindowX, (uint)WindowY), WindowName);
		window.SetFramerateLimit(60);
	}

	

	public void Begin()
    {
		PostLoad();
		SpawnBaseObjects();
		while (window.IsOpen)
        {
			GameCycle();
        }
    }

	private void SpawnBaseObjects()
    {
		foodSpawner = Spawn<AutomaticSpawner<Food>>();
		foodSpawner.Init(1000);
		botSpawner = Spawn<AutomaticSpawner<AIController>>();
		botSpawner.Init(2000);

		PlayerController notAI = Spawn<PlayerController>();

		for (int i = 0; i < BasePlayersAmount; i++)
			Spawn<AIController>();
    }

	public void GameCycle()
    {
		AgarioTime.UpdateDeltaTime();

		UpdateObjects();
		CheckCollision();

		DrawObjects();

		ClearDespawnList();
		SpawnObjects();
    }

	private void SpawnObjects()
    {
		foreach (var obj in objectsToSpawn)
        {
			AddToCollections(obj);
        }
		objectsToSpawn.Clear();
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

	private void AddToCollections(object gameObject)
    {
		drawableList.TryAddToCollection(gameObject);
		updatableList.TryAddToCollection(gameObject);
		collidableList.TryAddToCollection(gameObject);

		if (gameObject is DeletableObject deletable)
			SubscribeDeleteEvent(deletable);
	}

	private Type Spawn<Type>() where Type : new()
    {
		Type gameObject = new ();

		objectsToSpawn.Add(gameObject);

		return gameObject;
	}

	private void ClearDespawnList()
	{
		foreach (DeletableObject ToDespawn in objectsToDelete)
		{
			Despawn(ToDespawn);
		}

		objectsToDelete.Clear();
	}

	private void SubscribeDeleteEvent(DeletableObject Object)
    {
		Object.OnDestroy += AddToDespawnList;
    }

	private void UnSubscribeDeleteEvent(DeletableObject Object)
	{
		Object.OnDestroy -= AddToDespawnList;
	}

	private void AddToDespawnList(DeletableObject ToDespawn)
    {
		Console.WriteLine("Added to despawn list");
		objectsToDelete.Add(ToDespawn);
    }

	private void Despawn(DeletableObject ToDespawn)
    {
		drawableList.TryRemoveFromCollection(ToDespawn);
		updatableList.TryRemoveFromCollection(ToDespawn);
		collidableList.TryRemoveFromCollection(ToDespawn);
		UnSubscribeDeleteEvent(ToDespawn);
    }
}
