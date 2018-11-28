using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{
    public float parallaxSpeed;

    private Material backgroundMaterial;
    public Vector2 direction;

    void Start()
    {
        backgroundMaterial = GetComponent<MeshRenderer>().material;
    }

    void FixedUpdate()
    {
        backgroundMaterial.mainTextureOffset += direction * Time.fixedDeltaTime * parallaxSpeed;
    }
}
