using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpriteSwitch : MonoBehaviour
{
    SpriteRenderer planetSpriteRenderer;

    public Sprite planetFullHp;
    public Sprite planetAverageHp;
    public Sprite planetLowHp;
    public Sprite planetCriticalHp;


    // Use this for initialization
    void Start()
    {
        planetSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        planetSpriteRenderer.sprite = planetFullHp;
    }

    public void SwitchPlanetSprite(HealthState planetState)
    {
        switch (planetState)
        {
            case HealthState.full:
                planetSpriteRenderer.sprite = planetFullHp;
                break;
            case HealthState.average:
                planetSpriteRenderer.sprite = planetAverageHp;
                break;
            case HealthState.low:
                planetSpriteRenderer.sprite = planetLowHp;
                break;
            case HealthState.critical:
                planetSpriteRenderer.sprite = planetCriticalHp;
                break;
            default:
                break;
        }
    }
}
