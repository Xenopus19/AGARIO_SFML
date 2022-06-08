using SFML.System;
using SFML.Graphics;
using System;
using Agario.AdditionalTools;

namespace Agario.Controllers;


public class AIController : IController
{
    private Vector2f destination;

    private float destinatonChangeCooldown;
    private float cooldownPassed;
    public AIController()
    {
        destinatonChangeCooldown = 500;
        PickNewDestination();
    }

	public Vector2f GetDirection()
    {
        cooldownPassed += AgarioTime.DeltaTime;
        if(cooldownPassed>=destinatonChangeCooldown)
        {
            PickNewDestination();
        }

        return destination.Normalize();
    }

    private void PickNewDestination()
    {
        destination = Game.GetRandomPosition();
        if (AgarioRandom.NextInt(10) % 2 == 0)
            destination *= -1;
        cooldownPassed = 0;
    }
}
