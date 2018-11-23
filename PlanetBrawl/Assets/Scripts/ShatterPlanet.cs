using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterPlanet : MonoBehaviour
{
    public float radius = 5;
    public float explosionforce = 500f;
    private bool hasExploded = false;

    public GameObject explosion;

    public GameObject planetPartLeft;
    public GameObject planetPartRight;
    public GameObject planetPartBottom;

    private Rigidbody2D rbLeft;
    private Rigidbody2D rbRight;
    private Rigidbody2D rbBottom;

    public Vector2 forceLeft;
    public Vector2 forceRight;
    public Vector2 forceBottom;

    // Use this for initialization
    void Start()
    {
        rbLeft = planetPartLeft.AddComponent<Rigidbody2D>();
        rbRight = planetPartRight.AddComponent<Rigidbody2D>();
        rbBottom = planetPartBottom.AddComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (!hasExploded)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (planetPartLeft != null)
        {
            rbLeft.AddForce(forceLeft, ForceMode2D.Impulse);
        }
        if (planetPartRight != null)
        {
            rbRight.AddForce(forceRight);
        }
        if (planetPartBottom != null)
        {
            rbBottom.AddForce(forceBottom);
        }
    }
}
