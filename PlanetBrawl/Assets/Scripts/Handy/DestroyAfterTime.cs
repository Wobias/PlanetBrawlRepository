using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 5;

	void Start ()
    {
        Destroy(gameObject, lifetime);
	}
}
