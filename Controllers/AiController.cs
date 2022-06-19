using SFML.System;
using SFML.Graphics;
using System;
using Agario.AdditionalTools;
using Agario.GameObjects;

namespace Agario.Controllers;


public class AIController : Controller
{
    private Vector2f destination;

    private float destinatonChangeCooldown;
    private float cooldownPassed;
    public AIController() : base()
    {
        destinatonChangeCooldown = 500;
        PickNewDestination();
    }
    public override void SetPlayer(Player player)
    {
        this.player = player;
        player.SetOutlineThickness(0);
    }

    public override void CheckMovementInput()
    {
        cooldownPassed += AgarioTime.DeltaTime;
        if(cooldownPassed>=destinatonChangeCooldown)
        {
            PickNewDestination();
        }

        player.Move(destination.Normalize());
    }

    private void PickNewDestination()
    {
        destination = AgarioRandom.GetRandomPosition();
        if (AgarioRandom.NextInt(10) % 2 == 0)
            destination *= -1;
        cooldownPassed = 0;
    }
}
