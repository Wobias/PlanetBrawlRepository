using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalControllerRight : MonoBehaviour 
{
    Vector3 spawnPoint = Vector3.zero;
    BallMovement ballMovement;

    public Text pointsLeft;
    int points = 0;
    bool canScore = true;


    void OnTriggerEnter2D(Collider2D other)
    {
        ballMovement = other.GetComponent<BallMovement>();
        if (ballMovement != null && canScore)
        {
            points++;
            pointsLeft.text = points.ToString();

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


