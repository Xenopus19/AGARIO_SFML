using System;
using SFML.System;
using SFML.Window;
using Agario.GameObjects;

namespace Agario.Controllers;
public abstract class Controller : DeletableObject, IUpdatable
{
	public static Controller CurrentPlayerController;
	public Player player { get; protected set; }

	public Controller()
    {
		player = Game.Instantiate<Player>();
		player.OnDestroy += Die;
    }
	public void Update() 
	{
		CheckMovementInput();
		CheckShootingInput();
		CheckSoulRecast();
	}
	public virtual void CheckMovementInput() { }
	public virtual void CheckShootingInput() { }
	public virtual void SetPlayer(Player player) { }
	private static void Swap(Controller first, Controller second)
    {
		Player temp = first.player;
		first.SetPlayer(second.player);
		second.SetPlayer(temp);
		
    }
	private void CheckSoulRecast()
	{
		Vector2i mousePos = Mouse.GetPosition();
		if (player.GetCollider().Contains(mousePos.X, mousePos.Y) && CurrentPlayerController != this)
		{
			Console.WriteLine("Soul Recast");
			Swap(this, CurrentPlayerController);
		}
	}

	private void Die(DeletableObject deletable)
    {
		InvokeDeleteEvent();
    }
}

public struct Controls
{
	public Keyboard.Key UpKey;
	public Keyboard.Key LeftKey;
	public Keyboard.Key DownKey;
	public Keyboard.Key RightKey;

	public Keyboard.Key ShotKey;


	public Controls()
	{
		UpKey = Keyboard.Key.W;
		LeftKey = Keyboard.Key.A;
		DownKey = Keyboard.Key.S;
		RightKey = Keyboard.Key.D;

		ShotKey = Keyboard.Key.E;
	}

	public Controls(Keyboard.Key up, Keyboard.Key left, Keyboard.Key down, Keyboard.Key right, Keyboard.Key shot)
	{
		UpKey = up;
		LeftKey = left;
		DownKey = down;
		RightKey = right;

		ShotKey = shot;
	}
}
