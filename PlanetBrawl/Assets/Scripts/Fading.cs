using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public Image panel;
    // von anderen Scripts: Fading.instance.Function
    public static Fading instance;
    public float fadeInTime = 1;

    private float fadeTime;
    private bool fading = false;
    private float targetFade;

	void Awake ()
    {       
        instance = this;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);
	}

    private void OnEnable()
    {
        FadeOut(fadeInTime);
    }

    void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    FadeIn(2);
        //}
        //else if (Input.GetKeyDown(KeyCode.G))
        //{
        //    FadeOut(2);
        //}


        if (fading)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, Mathf.Lerp(panel.color.a, targetFade, Time.deltaTime / fadeInTime));

            if (panel.color.a == targetFade)
                fading = false;
        }
	}

    public void FadeIn(float time)
    {
        fading = true;
        fadeTime = time;
        targetFade = 1;
    }

    public void FadeOut(float time)
    {
        fading = true;
        fadeTime = time;
        targetFade = 0;
    }
}
