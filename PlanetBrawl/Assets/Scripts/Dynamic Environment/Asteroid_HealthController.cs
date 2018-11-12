using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_HealthController : HealthController
{
    protected AsteroidController controller;    


    protected override void Start()
    {
        base.Start();
        controller = GetComponent<AsteroidController>();
    }

    protected override void OnHealthChange(bool damage=true)
    {
        return;
    }

    protected override void Kill()
    {
        controller.Destroy();  
        base.Kill();
    }
}
