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
	}
	public virtual void GetDirection(Player player) { }
}

public struct Controls
{
	public Keyboard.Key UpKey;
	public Keyboard.Key LeftKey;
	public Keyboard.Key DownKey;
	public Keyboard.Key RightKey;

	public Controls()
	{
		UpKey = Keyboard.Key.W;
		LeftKey = Keyboard.Key.A;
		DownKey = Keyboard.Key.S;
		RightKey = Keyboard.Key.D;
	}

	public Controls(Keyboard.Key up, Keyboard.Key left, Keyboard.Key down, Keyboard.Key right)
	{
		UpKey = up;
		LeftKey = left;
		DownKey = down;
		RightKey = right;
	}
}
