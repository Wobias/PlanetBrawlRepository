using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    [Range(1,2)]
    public int team = 1;

    Vector3 spawnPoint = Vector3.zero;
    BallMovement ballMovement;
    IDamageable target;

    bool canScore = true;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = GameManager.instance.playerColors[team - 1];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ballMovement = other.GetComponent<BallMovement>();

        if (ballMovement != null && canScore)
        {
            if (team == 1)
                GameManager.instance.AddScore(1);
            else
                GameManager.instance.AddScore(0);

            canScore = false;

            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<TrailRenderer>().enabled = false;
            other.transform.position = spawnPoint;
            ballMovement.ResetSpeed();

            StartCoroutine(SpawnDelay(other.transform));
            return;
        }

        GetTarget(other);

        if (target != null)
        {
            Debug.Log("Gottem");
            target.Hit(100, DamageType.physical, Vector2.zero, 0.25f);
        }
    }

    IEnumerator SpawnDelay(Transform ball)
    {
        yield return new WaitForSeconds(0.5f);
        ball.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        ball.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        ball.gameObject.GetComponent<TrailRenderer>().enabled = true;
        canScore = true;
    }

    protected void GetTarget(Collider2D other)
    {
        target = null;
        target = other.GetComponent<IDamageable>();
        if (target == null)
        {
            target = other.attachedRigidbody?.GetComponent<IDamageable>();
        }
    }
}

