using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthHUD : MonoBehaviour
{
    public Sprite[] EarthSprites;

    public Image EarthHUI;

    private HealthController healthController;


	void Start ()
    {
        healthController = GameObject.FindGameObjectWithTag("HealthController").GetComponent<HealthController>();
	}
	
	void Update ()
    {
        //EarthHUI.sprite = EarthSprites[healthController.health];
	}
}
