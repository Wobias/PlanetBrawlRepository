using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public GameObject Ball;
    public TextMeshProUGUI teamScoreText;

    private int score = 0;
    private Vector3 center = new Vector3(0f,0f,0f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Ball)
        {
            score++;

            Ball.transform.position = center;
        }
    }
}
