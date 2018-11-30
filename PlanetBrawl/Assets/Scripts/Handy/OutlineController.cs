using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    public bool isParticle = false;

	void Start ()
    {
        PlayerController controller = transform.root.GetComponent<PlayerController>();

        if (!isParticle)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            if (renderer != null && controller != null)
            {
                renderer.color = controller.playerColor;
            }
        }
        else
        {
            ParticleSystem particleSystem = GetComponent<ParticleSystem>();

            if (particleSystem != null && controller != null)
            {
                particleSystem.startColor = controller.playerColor;
            }
        }
	}
}
