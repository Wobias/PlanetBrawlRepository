using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
	void Start ()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        PlayerController controller = transform.root.GetComponent<PlayerController>();

        if (renderer != null && controller != null)
        {
            renderer.color = controller.playerColor;
        }
	}
}
