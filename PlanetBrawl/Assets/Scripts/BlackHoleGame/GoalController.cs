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
                GameManager.instance.AddScore(2);
            else
                GameManager.instance.AddScore(1);

            canScore = false;

            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<TrailRenderer>().enabled = false;
            other.transform.position = spawnPoint;
            ballMovement.ResetSpeed();

            StartCoroutine(SpawnDelay(other.transform));
        }

    }
    IEnumerator SpawnDelay(Transform ball)
    {
        yield return new WaitForSeconds(2f);
        ball.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        ball.gameObject.GetComponent<TrailRenderer>().enabled = true;
        canScore = true;
    }       
}

