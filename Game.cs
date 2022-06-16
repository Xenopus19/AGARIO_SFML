﻿using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario.AdditionalTools;
using Agario.GameObjects;

namespace Agario;

public class Game
{
	public const int WINDOW_X = 1600;
	public const int WINDOW_Y = 900;
	public const string GAME_NAME = "Шарики-каннибалы";

	private static Game game;

	private RenderWindow window;

	private AgarioList<IUpdatable> updatableList;
	private AgarioList<ICollidable> collidableList;
	private AgarioList<IDrawable> drawableList;

	private List<DeletableObject> objectsToDelete;
	private List<object> objectsToSpawn;

	private float FoodCooldown;
	private float PassedCooldown;

	private int PlayersAmount;
	public Game()
	{
		game = this;

		window = new(new(WINDOW_X, WINDOW_Y), GAME_NAME);
		window.SetFramerateLimit(60);
		Graphics.LoadTextures();
		collidableList = new();
		drawableList = new();
		updatableList = new();
		objectsToDelete = new();
		objectsToSpawn = new();

		FoodCooldown = 1000;
		PassedCooldown = 0;

		PlayersAmount = 5;
	}

	public static T Instantiate<T>() where T : new()
    {
		return game.Spawn<T>();
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

		for (int i = 0; i < PlayersAmount; i++)
			Spawn<Player>();
    }

	public void GameCycle()
    {
		AgarioTime.UpdateDeltaTime();

		UpdateObjects();
		CheckCollision();
		TrySpawnFood();

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

	private void TrySpawnFood()
    {
		PassedCooldown += AgarioTime.DeltaTime;

		if(PassedCooldown>=FoodCooldown)
        {
			Spawn<Food>();
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
