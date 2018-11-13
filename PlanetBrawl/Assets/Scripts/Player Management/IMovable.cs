using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void ApplyGravForce(Vector2 force);
    void FlushGravForce();
    void ApplyTempExForce(Vector2 force, float time);
}
