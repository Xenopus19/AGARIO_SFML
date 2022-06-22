using System;
using Agario.AdditionalTools;

namespace Agario;
public class AutomaticSpawner<T> : IUpdatable where T : new()
{
    private float spawnCooldown;
    private float cooldownPassed;

    public void Update()
    {
        cooldownPassed += AgarioTime.DeltaTime;

        if(cooldownPassed>=spawnCooldown)
        {
            SpawnObject();
        }
    }

    public void Init(float cooldown)
    {
        spawnCooldown = cooldown;
    }

    private void SpawnObject()
    {
        cooldownPassed = 0;
        Game.Instantiate<T>();
    }


}
