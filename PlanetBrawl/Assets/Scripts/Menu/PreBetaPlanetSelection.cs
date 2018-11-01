using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreBetaPlanetSelection : MonoBehaviour
{
    public Button firstButton;
    public Button secondButton;
    public int currentPlanet = 0;
    public GameObject[] planets;
    private GameObject clone;


    private void Start()
    {

        currentPlanet = 0;
        firstButton.onClick.AddListener(Right);
        secondButton.onClick.AddListener(Left);
        clone = Instantiate(planets[currentPlanet]);
        clone.transform.SetParent(transform);
        clone.transform.position = new Vector3(-5f, 2.93f, -2f);
    }


    void Right()
    {
        if (currentPlanet < 3)
        {
            Destroy(clone);

            clone = Instantiate(planets[++currentPlanet]);
            clone.transform.SetParent(transform);
            clone.transform.position = new Vector3(-5f, 2.93f, -2f);
        }

    }

    void Left()
    {
        if (currentPlanet > 0)
        {
            Destroy(clone);

            clone = Instantiate(planets[--currentPlanet]);
            clone.transform.SetParent(transform);
            clone.transform.position = new Vector3(-5f, 2.93f, -2f);
        }
    }
}

