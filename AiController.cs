using SFML.System;
using SFML.Graphics;
using System;

namespace Agario;

public interface IController
{
	public Vector2f GetDirection();
}
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
        cooldownPassed += Time.DeltaTime;
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
