using System;
using SFML.System;
using SFML.Window;
using Agario.GameObjects;

namespace Agario.Controllers;
public abstract class Controller
{
	public void GetCommands(Player player) 
	{
		GetDirection(player);
		GetShotDirection(player);
	}
	public virtual void GetDirection(Player player) { }

	public virtual void GetShotDirection(Player player) { }

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
