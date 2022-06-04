using System;
using SFML.System;
using SFML.Window;

namespace Agario;
public class InputController
{
	private Controls controls;
	public InputController(Controls _controls)
	{
		controls = _controls;
	}

	public Vector2i GetInput()
    {
		Vector2i dir = new Vector2i();

		if (Keyboard.IsKeyPressed(controls.UpKey))
			dir.Y = -1;
		if (Keyboard.IsKeyPressed(controls.DownKey))
			dir.Y = 1;
		if (Keyboard.IsKeyPressed(controls.LeftKey))
			dir.X = -1;
		if (Keyboard.IsKeyPressed(controls.RightKey))
			dir.X = 1;

		return dir;
	}
}
