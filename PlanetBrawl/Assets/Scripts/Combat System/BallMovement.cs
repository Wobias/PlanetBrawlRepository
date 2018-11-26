using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour, IMovable
{
    Rigidbody2D rb2d;

    public float inputRolloff = 0.1f;
    public float forceRolloff = 0.1f;

    private Vector2 externalForce = Vector2.zero;
    private Vector2 targetExForce = Vector2.zero;
    private Vector2 gravForce = Vector2.zero;
    private Vector2 targetGravForce = Vector2.zero;

    public void ResetSpeed()
    {
        externalForce = Vector2.zero;
        targetExForce = Vector2.zero;
        gravForce = Vector2.zero;
        targetGravForce = Vector2.zero;

        rb2d.velocity = Vector2.zero;
        StopAllCoroutines();

    }

    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        externalForce = Vector2.Lerp(externalForce, targetExForce, forceRolloff);
        gravForce = Vector2.Lerp(gravForce, targetGravForce, forceRolloff);

        rb2d.velocity = externalForce + gravForce;
    }  
    
    IEnumerator AddExForce(Vector2 force, float time)
    {
        targetExForce += force;
        externalForce = targetExForce;
        yield return new WaitForSeconds(time);
        targetExForce -= force;
    }

    public void FlushGravForce()
    {
        targetGravForce = Vector2.zero;
    }
    public void ApplyGravForce(Vector2 force)
    {
        targetGravForce += force;
    }

    public void ApplyTempExForce(Vector2 force, float time)
    {
        StartCoroutine(AddExForce(force, time));
    }


}
