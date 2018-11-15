using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone_Fade : MonoBehaviour
{
    public SpriteRenderer[] fadeRenderers;
    public float fadeTime;


    private void FixedUpdate()
    {
        for (int i = 0; i < fadeRenderers.Length; i++)
        {
            fadeRenderers[i].color -= new Color(0, 0, 0, Time.fixedDeltaTime / fadeTime);
        }

        if (fadeRenderers[0].color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
