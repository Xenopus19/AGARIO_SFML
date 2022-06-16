using System;
using SFML.System;
using SFML.Window;
using Agario.GameObjects;

namespace Agario.Controllers;
public class PlayerController : Controller
{
	private Controls controls;
	public PlayerController(Controls _controls) 
	{
		controls = _controls;
	}

	public override void GetDirection(Player player)
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

		 player.Move((Vector2f)dir);
	}

    public override void GetShotDirection(Player player)
    {
        if(Keyboard.IsKeyPressed(controls.ShotKey))
        {
			Vector2f dir = (Vector2f)Mouse.GetPosition();
			player.MakeShot(dir);
        }
    }
}
