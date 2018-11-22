using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSpriteSwitch : MonoBehaviour
{
    SpriteRenderer faceSpriteRenderer;

    public Sprite faceFullHp;
    public Sprite faceAverageHp;
    public Sprite faceLowHp;
    public Sprite faceCriticalHp;
    public Sprite faceHit;

    private Sprite mySprite;

    void Start()
    {
        faceSpriteRenderer = GetComponent<SpriteRenderer>();
        faceSpriteRenderer.sprite = faceFullHp;
    }

    public void SwitchFaceSprite(HealthState planetState)
    {
        switch (planetState)
        {
            case HealthState.full:
                faceSpriteRenderer.sprite = faceFullHp;
                break;
            case HealthState.average:
                faceSpriteRenderer.sprite = faceAverageHp;
                break;
            case HealthState.low:
                faceSpriteRenderer.sprite = faceLowHp;
                break;
            case HealthState.critical:
                faceSpriteRenderer.sprite = faceCriticalHp;
                break;
            default:
                break;
        }
    }

    public IEnumerator FaceHit()
    {
        faceSpriteRenderer.sprite = faceHit;
        yield return new WaitForSeconds(0.2f);
        SwitchFaceSprite(Player_HealthController.healthState);
    }
}
