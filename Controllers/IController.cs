using System;
using SFML.System;
using SFML.Window;

namespace Agario.Controllers;
public interface IController
{
	public Vector2f GetDirection();
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
