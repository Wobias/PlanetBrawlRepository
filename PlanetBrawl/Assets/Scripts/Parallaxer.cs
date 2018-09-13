using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{
    public float parallaxSpeed;

    private Material backgroundMaterial;
    private Vector2 startOffset;
    private Vector2 offset;

    void Start()
    {
        backgroundMaterial = GetComponent<MeshRenderer>().material;

        startOffset = backgroundMaterial.mainTextureOffset;
    }

    void LateUpdate()
    {
        offset = new Vector2(transform.position.x * 0.01f * parallaxSpeed, transform.position.y * 0.01f * parallaxSpeed);
        backgroundMaterial.mainTextureOffset = offset + startOffset;
    }
}
